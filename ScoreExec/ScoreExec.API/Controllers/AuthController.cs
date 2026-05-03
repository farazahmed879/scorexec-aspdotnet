using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreExec.API.Models.Auth;
using ScoreExec.Application.Common.Interfaces;
using ScoreExec.Domain.Entities;
using ScoreExec.Domain.Enums;
using ScoreExec.Infrastructure.Persistence;
using ScoreExec.Infrastructure.Services;

namespace ScoreExec.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TokenService _tokenService;
    private readonly IApplicationDbContext _context;

    public AuthController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        TokenService tokenService,
        IApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var tenant = Tenant.Create(request.TenantName, request.TenantSlug);
        await _context.Tenants.AddAsync(tenant);
        await _context.SaveChangesAsync();

        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            TenantId = tenant.Id,
            Role = UserRole.TenantAdmin
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { errors });
        }

        var token = _tokenService.GenerateToken(user);

        return Ok(new AuthResponse(
            token,
            user.Email!,
            user.FullName,
            user.Role.ToString(),
            user.TenantId,
            DateTime.UtcNow.AddMinutes(60)
        ));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        AppUser? user = null;

        if (request.Email.Contains("@"))
        {
            user = await _userManager.FindByEmailAsync(request.Email);
        }
        else
        {
            user = await _userManager.FindByNameAsync(request.Email);
            if (user == null)
            {
                // Try phone number
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.Email);
            }
        }

        if (user is null || user.IsDeleted)
        {
            return Unauthorized(new { error = "Invalid credentials." });
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized(new { error = "Invalid credentials." });
        }

        var token = _tokenService.GenerateToken(user);

        return Ok(new AuthResponse(
            token,
            user.Email!,
            user.FullName,
            user.Role.ToString(),
            user.TenantId,
            DateTime.UtcNow.AddMinutes(60)
        ));
    }
}
