using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeightTracker.Api.Helpers
{
    public class JwtHelper
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly IConfiguration _config;

        public JwtHelper(JwtSecurityTokenHandler tokenHandler, IConfiguration config)
        {
            _tokenHandler = tokenHandler;
            _config = config;
        }

        public string CreateToken(string name)
        {
            var claims = CreateClaims(name);
            var credetials = CreateCredetials();

            var token = new JwtSecurityToken(
              _config.GetValue<string>("Jwt:Issuer"),
              _config.GetValue<string>("Jwt:Issuer"),
              claims: claims,
              expires: DateTime.Now.AddMinutes(100),
              signingCredentials: credetials
           );

            return _tokenHandler.WriteToken(token);
        }

        public bool VerifyToken(string token)
        {
            try
            {
                var validationParams = GetTokenValidationParameters();
                ClaimsPrincipal resultToken = _tokenHandler.ValidateToken(token, validationParams, out SecurityToken securityToken);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private IEnumerable<Claim> CreateClaims(string name)
        {
            /*
            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Email, "test@gmail.com"));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, $"{user.FirstName} {user.LastName}"));
            claims.AddClaim(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")); */

            /*
            return new[] {
                new Claim(JwtRegisteredClaimNames.GivenName, $"{user.FirstName} {user.LastName}")
            }; */

            return new[] {
                new Claim(JwtRegisteredClaimNames.GivenName, $"{name}")
            };
        }

        private SigningCredentials CreateCredetials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key")));
            var credetials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return credetials;
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            /*
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidAudience = "https://houseofcat.io", // these values derive from the Create JWT token guide (don't copy these!)
                ValidIssuer = "https://houseofcat.io", // these values derive from the Create JWT token guide (don't copy these!)
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                LifetimeValidator = this.LifetimeValidator,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.Default // these values derive from the Create JWT token guide (don't copy these!)
                    .GetBytes("J6k2eVCTXDp5b97u6gNH5GaaqHDxCmzz2wv3PRPFRsuW2UavK8LGPRauC4VSeaetKTMtVmVzAC8fh8Psvp8PFybEvpYnULHfRpM8TA2an7GFehrLLvawVJdSRqh2unCnWehhh2SJMMg5bktRRapA8EGSgQUV8TCafqdSEHNWnGXTjjsMEjUpaxcADDNZLSYPMyPSfp6qe5LMcd5S9bXH97KeeMGyZTS2U8gp3LGk2kH4J4F3fsytfpe9H9qKwgjb"))
            }; */

            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidAudience = _config.GetValue<string>("Jwt:Issuer"),
                ValidIssuer = _config.GetValue<string>("Jwt:Issuer"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key"))),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
               // ValidateLifetime = true,
                RequireExpirationTime = true
            };

            return validationParameters;
        }

        /*
        private bool LifetimeValidator(
            DateTime? notBefore,
            DateTime? expires,
            SecurityToken securityToken,
            TokenValidationParameters validationParameters)
        {
            var valid = false;

            if ((expires.HasValue && DateTime.UtcNow < expires)
                && (notBefore.HasValue && DateTime.UtcNow > notBefore))
            { valid = true; }

            return valid;
        } */
    }
}
