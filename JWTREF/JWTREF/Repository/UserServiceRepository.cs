using JWTREF.Model;
using Microsoft.AspNetCore.Identity;

namespace JWTREF.Repository
{
    public class UserServiceRepository : IUserServiceRepository
    {
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _db;

        public UserServiceRepository(AppDbContext db)
        {
            this._db = db;
        }

        public UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user)
        {
            _db.UserRefreshToken.Add(user);
            return user;
        }

        public void DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = _db.UserRefreshToken.FirstOrDefault(x => x.Username == username && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _db.UserRefreshToken.Remove(item);
            }
        }

        public UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken)
        {
            return _db.UserRefreshToken.FirstOrDefault(x => x.Username == username && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public int SaveCommit()
        {
            return _db.SaveChanges();
        }

        public async Task<Users> IsValidUserAsync(Users users)
        {
            var result = _db.User.FirstOrDefault(o => o.Username == users.Username && o.Password == users.Password);
            
            return result;

        }
    }
}
