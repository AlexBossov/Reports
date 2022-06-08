using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Security.Tokens
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(Employee employee);
        RefreshToken TakeRefreshToken(string token);
        void RevokeRefreshToken(string token);
    }
}