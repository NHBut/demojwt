using JWTREF.Model;

namespace JWTREF.Repository
{
    public interface IUserServiceRepository
    {
        Task<Users> IsValidUserAsync(Users users);

        UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user);

        UserRefreshTokens GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);

        int SaveCommit();
    }
}
