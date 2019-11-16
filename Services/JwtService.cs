using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Services
{
    #pragma warning disable CS1591
    public class JwtService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly IConfiguration _config;

        public JwtService(JwtSecurityTokenHandler tokenHandler, IConfiguration config)
        {
            _tokenHandler = tokenHandler;
            _config = config;
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            var jwtKey = _config.GetSection("Jwt").GetSection("Key").Value;
            var secret = Encoding.ASCII.GetBytes(jwtKey);

            var issuer = _config.GetSection("Jwt").GetSection("Issuer").Value;
            var audience = _config.GetSection("Jwt").GetSection("Audience").Value;

            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secret),
                ValidIssuer = issuer,
                ValidAudience = audience,
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        /*
        public string CreateToken(string name)
        {
            var claim = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Admin2"),
                new Claim(ClaimTypes.Role, "SuperUser")
            };
            var jwtKey = _config.GetSection("Jwt").GetSection("Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var issuer = _config.GetSection("Jwt").GetSection("Issuer").Value;
            var audience = _config.GetSection("Jwt").GetSection("Audience").Value;

            var accessExpiration = _config.GetSection("Jwt").GetSection("AccessExpiration").Value;
            Double accessExpirationDouble = Convert.ToDouble(accessExpiration);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer,
                audience,
                claim,
                expires: DateTime.Now.AddMinutes(accessExpirationDouble),
                signingCredentials: credentials
            );
            return _tokenHandler.WriteToken(jwtToken);
        } */

            /*
        public string CreateToken(string name)
        {
            var claim = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Admin2"),
                new Claim(ClaimTypes.Role, "SuperUser")
            };
            return CreateToken(claim);
        } */

        public string CreateToken(UserModel user)
        {
            var claim = new[]
            {
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName)
            };
            return CreateToken(claim);
        }

        public string CreateToken(IEnumerable<Claim> claim)
        {
            var jwtKey = _config.GetSection("Jwt").GetSection("Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var issuer = _config.GetSection("Jwt").GetSection("Issuer").Value;
            var audience = _config.GetSection("Jwt").GetSection("Audience").Value;

            var accessExpiration = _config.GetSection("Jwt").GetSection("AccessExpiration").Value;
            Double accessExpirationDouble = Convert.ToDouble(accessExpiration);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer,
                audience,
                claim,
                expires: DateTime.Now.AddMinutes(accessExpirationDouble),
                signingCredentials: credentials
            );
            return _tokenHandler.WriteToken(jwtToken);
        }
    }
    #pragma warning restore CS1591
}
