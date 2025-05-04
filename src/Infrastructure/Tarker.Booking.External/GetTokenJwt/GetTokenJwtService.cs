using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tarker.Booking.Application.External.GetTokenJwt;

namespace Tarker.Booking.External.GetTokenJwt;

public class GetTokenJwtService : IGetTokenJwtService
{
    private readonly IConfiguration _configuration;
    public GetTokenJwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    //xGUmu3zO3z2X00oUOaao7faCy8vwNRu2vnV
    public string Execute(string id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        string key = _configuration["SecretKeyJwt"] ?? string.Empty;
        var signinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]{
                new (ClaimTypes.NameIdentifier, id)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new (signinkey, SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["IssuerJwt"] ?? string.Empty,
            Audience = _configuration["AudienceJwt"] ?? string.Empty
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}
