using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Mem.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Mem.Models;
using Microsoft.Extensions.Configuration;

namespace Mem.Controllers
{
   
    public class UserController : Controller
    {
        private readonly UserService userService;
        private readonly IConfiguration configuration;

        public UserController(UserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        [Authorize]
        public IActionResult Ping()
        {
            return Json(true);
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel login)
        {
            if (!Regex.IsMatch(login.username, "[a-zA-Z0-9_.@]+"))
                return StatusCode(400);

            if (!userService.ValidatePassword(login.username, login.password))
                return StatusCode(404);

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var _refresh_secret = configuration["App:Refresh_Secret"].Split('|').Select(i => byte.Parse(i)).ToArray();

            UserModel user = userService.GetUser(login.username);
            var identity = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()) });

            SecurityTokenDescriptor tokenDescriptor = CreateTokenDescriptor(identity);

            var refresh_tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_refresh_secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refresh_token = tokenHandler.CreateToken(refresh_tokenDescriptor);

            Response.Cookies.Append("refreh_token", tokenHandler.WriteToken(refresh_token), new CookieOptions() { HttpOnly = true });

            return Json(new
            {
                token = tokenHandler.WriteToken(token),
                loggeduser = user.Name,
                loggeduserid = user.ID,
                username = user.Username
            });
        }

        [HttpPost]
        private SecurityTokenDescriptor CreateTokenDescriptor(ClaimsIdentity identity)
        {
            var _secret = configuration["App:Secret"].Split('|').Select(i => byte.Parse(i)).ToArray();

            return new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        public IActionResult RefreshToken()
        {
            if (!Request.Cookies.Any(c => "refreh_token" == c.Key))
                return StatusCode(404);

            var refresh_token = Request.Cookies.First(c => "refreh_token" == c.Key).Value;
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var claim = ValidateRefresh_Token(tokenHandler, refresh_token);
                var userId = claim.FindFirst(ClaimTypes.NameIdentifier).Value;
                UserModel user = userService.GetUser(int.Parse(userId));
                var identity = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()) });
                var token = tokenHandler.CreateToken(CreateTokenDescriptor(identity));

                return Json(new
                {
                    token = tokenHandler.WriteToken(token),
                    loggeduser = user.Name,
                    loggeduserid = user.ID,
                    username = user.Username
                });
            }
            catch (Exception)
            {
                return StatusCode(404);
            }
        }

        private ClaimsPrincipal ValidateRefresh_Token(JwtSecurityTokenHandler tokenHandler,string refresh_token)
        {
            var _refresh_secret = configuration["App:Refresh_Secret"].Split('|').Select(i => byte.Parse(i)).ToArray();
            var _params = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_refresh_secret),
                RequireSignedTokens = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                {
                    if (expires != null && expires.Value > DateTime.UtcNow)
                        return true;
                    else
                        return false;
                }
            };
            SecurityToken refreshToken;
            return tokenHandler.ValidateToken(refresh_token, _params, out refreshToken);
        }
    }
}