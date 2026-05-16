
using Microsoft.AspNetCore.Identity;
using SurveyBasket.Api.Authentication;
using System.Security.Cryptography;

namespace SurveyBasket.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTProvider _jWTProvider;
        private readonly int _refreshTokenExpiryDayes = 15;
        public AuthService(UserManager<ApplicationUser> userManager,IJWTProvider jWTProvider)
        {
            _userManager = userManager;
           _jWTProvider = jWTProvider;
        }
        public async Task<AuthenticationResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            // check user ?
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;
            //check password 
            var isValidePassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidePassword)
                return null;
            //get token
            var (token, expiresIn) = _jWTProvider.GenerateToken(user);

            // Add refreshToken dataToResponse and save values to DB
            var refreshToken =   GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDayes);
            user.RefreshTokens.Add(new RefreshToken
            {
                RefreshTokenValue = refreshToken,
                ExpiresOn = refreshTokenExpiration
            });
            await _userManager.UpdateAsync(user);

            //return new AuthenticationResponse(),
            return new AuthenticationResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn,refreshToken, refreshTokenExpiration);
        }


        public async Task<AuthenticationResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jWTProvider.ValidateToken(token);
            if (userId is null)
                return null;

            var user =await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.RefreshTokenValue == refreshToken && x.IsActive);
            if (userRefreshToken == null) return null;

            userRefreshToken.RevokedOn = DateTime.UtcNow;
            //get New token and New Refresh Token
            var (newToken, expiresIn) = _jWTProvider.GenerateToken(user);

            // Add refreshToken dataToResponse and save values to DB
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDayes);
            user.RefreshTokens.Add(new RefreshToken
            {
                RefreshTokenValue = newRefreshToken,
                ExpiresOn = refreshTokenExpiration
            });
            await _userManager.UpdateAsync(user);
             
            //return new AuthenticationResponse(),
            return new AuthenticationResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expiresIn, newRefreshToken, refreshTokenExpiration);



        }



     
        public async Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId =  _jWTProvider.ValidateToken(token);
            if (userId is null)
                return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.RefreshTokenValue == refreshToken && x.IsActive);
            if (userRefreshToken == null) return false;

            userRefreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return true;
        }


        private static string GenerateRefreshToken()
        {
            var refreshtokenGenarator = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(refreshtokenGenarator);
            return refreshToken;
        }
    }
}
