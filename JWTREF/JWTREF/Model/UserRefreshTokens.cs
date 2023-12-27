using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTREF.Model
{
    public class UserRefreshTokens
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; } = true;
        public Users User { get; set; }
    }
}
