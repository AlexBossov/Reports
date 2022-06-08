using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Security.Hashing;
using BusinessLogicLayer.Security.Tokens;
using BusinessLogicLayer.Services.Communication;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;
        
        public AuthenticationService(
            IEmployeeService employeeService, 
            IPasswordHasher passwordHasher, 
            ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _passwordHasher = passwordHasher;
            _employeeService = employeeService;
        }

        public async Task<TokenResponse> CreateAccessTokenAsync(string email, string password)
        {
            Employee employee = await _employeeService.FindByEmailAsync(email);

            if (employee == null 
                || !_passwordHasher.PasswordMatches(password, employee.Password))
            {
                return new TokenResponse(false, "Invalid credentials.", null);
            }

            AccessToken token = _tokenHandler.CreateAccessToken(employee);

            return new TokenResponse(true, null, token);
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail)
        {
            RefreshToken token = _tokenHandler.TakeRefreshToken(refreshToken);

            if (token == null)
            {
                return new TokenResponse(false, "Invalid refresh token.", null);
            }

            if (token.IsExpired())
            {
                return new TokenResponse(false, "Expired refresh token.", null);
            }

            Employee user = await _employeeService.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return new TokenResponse(false, "Invalid refresh token.", null);
            }

            AccessToken accessToken = _tokenHandler.CreateAccessToken(user);
            return new TokenResponse(true, null, accessToken);
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }
    }
}