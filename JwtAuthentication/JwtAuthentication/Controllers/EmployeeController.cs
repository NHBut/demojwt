using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("GetData")]
        public string GetData()
        {
            return "Authenthicataed with jwt";
        }
        [HttpGet]
        [Route("Detail")]
        public string details()
        {
            return "Authenticated with jwt";

        }
        [Authorize]
        [HttpPost]
        public String AddUser(Users user)
        {
            return "User added with username" + user.username;

        }
    }
}
