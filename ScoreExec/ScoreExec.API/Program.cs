using Microsoft.OpenApi.Models;
using ScoreExec.API.Middleware;
using ScoreExec.Application;
using ScoreExec.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Environment.EnvironmentName = "Development";

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ScoreExec API",
        Version = "v1",
        Description = "Sports Scoring SaaS Platform API"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var configuredUrls = builder.Configuration["ASPNETCORE_URLS"] ?? builder.Configuration["urls"];
if (string.IsNullOrWhiteSpace(configuredUrls))
{
    builder.WebHost.UseUrls("http://localhost:5094");
}

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScoreExec API v1"));

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Lifetime.ApplicationStarted.Register(() =>
{
    Log.Information(
        "ScoreExec API started successfully in {Environment} mode. URLs: {Urls}",
        app.Environment.EnvironmentName,
        string.Join(", ", app.Urls));
});

app.Run();
