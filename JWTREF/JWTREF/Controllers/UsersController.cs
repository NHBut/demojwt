using JWTREF.Model;
using JWTREF.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWTREF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IJWTManagerRepository jWTManager;
        private readonly IUserServiceRepository userServiceRepository;
        private readonly AppDbContext _db;

        public UsersController(IJWTManagerRepository jWTManager, IUserServiceRepository userServiceRepository, AppDbContext db)
        {
            this.jWTManager = jWTManager;
            this.userServiceRepository = userServiceRepository;
            this._db = db;
        }
        [Authorize]
        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string>
        {
            "Satinder Singh",
            "Amit Sarna",
            "Davin Jon"
        };

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(CreateUserInputModel usersdata)
        {
            var validUser = _db.User.FirstOrDefault(x => x.Username == usersdata.Username);

            if (validUser != null)
            {
                return BadRequest("Cannot add user vi co roi");
            }

            var newUser = new Users { Username = usersdata.Username, Password = usersdata.Password };
            var result = _db.User.Add(newUser);
            _db.SaveChanges();
            if (result == null)
                return BadRequest("Cannot add user");
            return Ok("bu");
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(CreateUserInputModel usersdata)
        {
            var validUser = _db.User.FirstOrDefault(x => x.Username == usersdata.Username);

            if (validUser == null)
            {
                return Unauthorized("Incorrect username or password!");
            }



            var token = jWTManager.GenerateToken(usersdata.Username);

            if (token == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            // saving refresh token to the db
            UserRefreshTokens obj = new UserRefreshTokens
            {
                RefreshToken = token.Refresh_Token,
                Username = usersdata.Username
            };

            var check = _db.UserRefreshToken.FirstOrDefault(x => x.Username == usersdata.Username);
            if (check == null)
            {
                userServiceRepository.AddUserRefreshTokens(obj);
                userServiceRepository.SaveCommit();
            }
            check.RefreshToken = token.Refresh_Token;

            _db.SaveChanges();



            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(Tokens token)
        {
            var principal = jWTManager.GetPrincipalFromExpiredToken(token.Access_Token);
            var username = principal.Identity?.Name;

            //retrieve the saved refresh token from database
            var savedRefreshToken = _db.UserRefreshToken.FirstOrDefault(x => x.Username==username &&
            x.RefreshToken == token.Refresh_Token &&
            x.IsActive == true);

            if (savedRefreshToken == null || savedRefreshToken.RefreshToken != token.Refresh_Token)
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = jWTManager.GenerateRefreshToken(username);

            if (newJwtToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            // saving refresh token to the db
            UserRefreshTokens obj = new UserRefreshTokens
            {
                RefreshToken = newJwtToken.Refresh_Token,
                Username = username
            };

            userServiceRepository.DeleteUserRefreshTokens(username, token.Refresh_Token);
            userServiceRepository.AddUserRefreshTokens(obj);
            userServiceRepository.SaveCommit();

            return Ok(newJwtToken);
        }
    }
}
