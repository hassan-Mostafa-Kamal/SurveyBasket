
namespace SurveyBasket.Api.Services
{
    public interface IAuthService
    {
        Task<AuthenticationResponse?> GetTokenAsync(string email , string password,CancellationToken cancellationToken = default);
        Task<AuthenticationResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);


    }
}
