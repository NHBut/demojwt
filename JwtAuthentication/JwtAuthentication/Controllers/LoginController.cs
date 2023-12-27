using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration configuration)
        {
            _config= configuration;
        }
        private Users AuthenticateUser(Users user)
        {
            Users _user = null;
            if (user.username == "admin" && user.password == "123456")
            {
                _user = new Users { username = "Nguyen Van But" };
            }

            return _user;
        }
        private String GenerateToken(Users users)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users user)
        {
            IActionResult response = Unauthorized("Sai tai khoan hoac mat khau");
            var user_ = AuthenticateUser(user);
            if(user_ != null)
            {
                var token = GenerateToken(user_);
                response= Ok(new { token = token });
            }
            return response;

        }
    }  
}
