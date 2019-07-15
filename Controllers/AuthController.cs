using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers
{
    public class AuthController : ControllerBase
    {
        [HttpPut("login")]
        public IActionResult Login([FromBody]Login login)
        {
            bool existUser = (login.Username == "user" && login.Password == "pwd");

            if (existUser)
            {
                var requestAt = DateTime.Now;
                var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                var token = GenerateToken(login, expiresIn);

                return Ok(
                    new
                    {
                        requertAt = requestAt,
                        expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                        tokeyType = TokenAuthOption.TokenType,
                        accessToken = token
                    }
                );
            }
            else
            {
                //return Unauthorized();
                // return StatusCode(401, new{
                //     Msg = "Username or password is invalid"
                // });
                return StatusCode(401, "Username or password is invalid");
            }
        }

        private string GenerateToken(Login login, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(login.Username, "TokenAuth"),
                new[] {
                    new Claim(ClaimTypes.Name, login.Username.ToString()),
                    new Claim(ClaimTypes.Role, "dev")
                    }
            );

            var securityToken = handler
            .CreateToken(
                new SecurityTokenDescriptor
                {
                    Issuer = TokenAuthOption.Issuer,
                    Audience = TokenAuthOption.Audience,
                    SigningCredentials = TokenAuthOption.SigningCredentials,
                    Subject = identity,
                    Expires = expires
                });
            return handler.WriteToken(securityToken);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;


            return Ok(new
            {
                UserName = claimsIdentity.Name,
                Role = claimsIdentity.Claims.Single(
                    m => m.Type == ClaimTypes.Role).Value
            });
        }

        [AllowAnonymous]
        [HttpGet("anonymous")]
        public IActionResult Anonymous()
        {
            return new ContentResult() { Content = $@"For all anonymous." };
        }
    }
}