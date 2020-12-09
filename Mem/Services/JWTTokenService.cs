using Mem.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mem.Services
{
    public class JWTTokenService
    {
        private readonly IConfiguration configuration;

        public JWTTokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()) });
            SecurityTokenDescriptor tokenDescriptor = CreateTokenDescriptor(identity);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetRefreshToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()) });
            SecurityTokenDescriptor tokenDescriptor = CreateRefreshTokenDescriptor(identity);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public (bool, ClaimsPrincipal) ValidateToken(string token)
        {
            try
            {
                var _secret = Secret(configuration);
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken _token;
                var claim = tokenHandler.ValidateToken(token, StandardValidationParameters(configuration), out _token);
                return (true, claim);
            }
            catch (Exception)
            {
                return (false, null);
            }
        }

        public static bool LifeTimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null && expires.Value > DateTime.UtcNow)
                return true;
            else
                return false;
        }

        public static TokenValidationParameters StandardValidationParameters(IConfiguration configuration)
        {
            var _secret = Secret(configuration);
            return new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_secret),
                RequireSignedTokens = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                LifetimeValidator = JWTTokenService.LifeTimeValidator,
                ValidAudience= configuration["App:Audience"],
                ValidIssuer = configuration["App:Issuer"]
            };
        }


        private SecurityTokenDescriptor CreateTokenDescriptor(ClaimsIdentity identity)
        {
            var _secret = Secret(configuration);
            var expires = int.Parse(configuration["App:Expires"]);
            return new SecurityTokenDescriptor
            {
                Subject = identity,
                Audience = configuration["App:Audience"],
                Issuer = configuration["App:Issuer"],
                Expires = DateTime.UtcNow.AddMinutes(expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private SecurityTokenDescriptor CreateRefreshTokenDescriptor(ClaimsIdentity identity)
        {
            var _secret = Secret(configuration);
            var expires = int.Parse(configuration["App:RefreshExpire_HH"]);
            return new SecurityTokenDescriptor
            {
                Subject = identity,
                Audience = configuration["App:Audience"],
                Issuer = configuration["App:Issuer"],
                Expires = DateTime.UtcNow.AddHours(expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private static byte[] Secret(IConfiguration configuration)
        {
            var s = configuration["App:Secret"];
            return Encoding.ASCII.GetBytes(s);
        }


    }
}
