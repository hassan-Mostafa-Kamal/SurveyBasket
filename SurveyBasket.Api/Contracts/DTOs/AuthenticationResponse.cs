namespace SurveyBasket.Api.Contracts.DTOs
{
    public record AuthenticationResponse(
        string Id,
        string? Email,
        string FirstName,
        string LastName,
        string Token,
        int ExpiresIn,
        // add refresh token prop
        string RefreshToken,
        DateTime RefreshTokenExpiration

        );
    
}
