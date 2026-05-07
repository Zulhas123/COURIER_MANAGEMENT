using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourierManagement.Web.Controllers.Api;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public sealed class TokenRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    [HttpPost("token")]
    public IActionResult Token([FromBody] TokenRequest request)
    {
        var expectedUser = _configuration["ApiAuth:Username"];
        var expectedPass = _configuration["ApiAuth:Password"];

        if (string.IsNullOrWhiteSpace(expectedUser) || string.IsNullOrWhiteSpace(expectedPass))
        {
            return Problem("API auth credentials are not configured.");
        }

        if (!string.Equals(request.Username, expectedUser, StringComparison.Ordinal) ||
            !string.Equals(request.Password, expectedPass, StringComparison.Ordinal))
        {
            return Unauthorized();
        }

        var jwtSection = _configuration.GetSection("Jwt");
        var issuer = jwtSection.GetValue<string>("Issuer") ?? "CourierManagement";
        var audience = jwtSection.GetValue<string>("Audience") ?? "CourierManagement";
        var key = jwtSection.GetValue<string>("Key") ?? throw new InvalidOperationException("Jwt:Key missing.");

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var now = DateTime.UtcNow;
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims:
            [
                new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
            ],
            notBefore: now,
            expires: now.AddHours(2),
            signingCredentials: credentials
        );

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            tokenType = "Bearer",
            expiresAtUtc = token.ValidTo
        });
    }
}

