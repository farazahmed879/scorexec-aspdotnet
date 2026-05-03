using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreExec.Domain.Enums;

namespace ScoreExec.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RolesController : ControllerBase
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RolesController(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return Ok(roles);
    }

    [HttpGet("enum")]
    public IActionResult GetEnumRoles()
    {
        var roles = Enum.GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .Select(r => new { Id = (int)r, Name = r.ToString() });
        return Ok(roles);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string roleName)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        return result.Succeeded ? Ok() : BadRequest(result.Errors);
    }
}
