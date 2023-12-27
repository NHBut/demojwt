using System.ComponentModel.DataAnnotations;

namespace JWTREF.Model
{
    public class Users
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRefreshTokens UserRefreshToken { get; set; }
    }
}
