using Billboard.Data;
using Billboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Billboard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration;
        public UserDbContext _userDbContext;


        public AuthController(IConfiguration configuration, UserDbContext context)
        {
            _userDbContext = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(usercred user)
        {
            if (user != null && user.username != null && user.password != null)
            {
                var userData = await GetUser(user.username, user.password);
                var jwt = _configuration.GetSection("JWTSetting").Get<JWTSetting>();
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.username),
                        new Claim("Password", user.password)

                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                       jwt.issuer,
                       jwt.audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                    );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }
        [HttpGet]
        public async Task<usercred> GetUser(string username, string password)
        {
            return await _userDbContext.usercred.FirstOrDefaultAsync(u => u.username == username && u.password == password);
        }
    }
}



//[Route("Authenticate")]
//        [HttpPost]
//        public IActionResult Authenticate([FromBody] usercred user)
//        {
//            var User = _userDbContext.usercred.FirstOrDefault(o => o.username == user.username && o.password == user.password);
//            if (User == null)
//            {
//                return Unauthorized();
//            }
//            var tokenhandler = new JwtSecurityTokenHandler();
//            var tokenkey = Encoding.UTF8.GetBytes(_jwtsetting.securitykey);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(
//                    new Claim[]
//                    {
//                        new Claim(ClaimTypes.Name, User.username )


//                    }
//                ),
//                Expires = DateTime.Now.AddMinutes(20),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
//            };
//            var token = tokenhandler.CreateToken(tokenDescriptor);
//            string finaltoken = tokenhandler.WriteToken(token);

//            return Ok(finaltoken);
//        }
//    }
//}
