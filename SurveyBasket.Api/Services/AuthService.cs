
using Microsoft.AspNetCore.Identity;
using SurveyBasket.Api.Authentication;

namespace SurveyBasket.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTProvider _jWTProvider;

        public AuthService(UserManager<ApplicationUser> userManager,IJWTProvider jWTProvider)
        {
            _userManager = userManager;
           _jWTProvider = jWTProvider;
        }
        public async Task<AuthenticationResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            // check user ?
            var user =await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;
            //check password 
            var isValidePassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidePassword)
                return null;
            //get token
            var (token, expiresIn) = _jWTProvider.GenerateToken(user);
            //return new AuthenticationResponse(),
            return new AuthenticationResponse(user.Id,user.Email,user.FirstName,user.LastName,token,expiresIn);
        }
    }
}
