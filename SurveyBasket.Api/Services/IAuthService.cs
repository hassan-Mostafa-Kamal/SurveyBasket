
namespace SurveyBasket.Api.Services
{
    public interface IAuthService
    {
        Task<AuthenticationResponse?> GetTokenAsync(string email , string password,CancellationToken cancellationToken = default);
    }
}
