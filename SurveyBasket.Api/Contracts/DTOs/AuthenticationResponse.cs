namespace SurveyBasket.Api.Contracts.DTOs
{
    public record AuthenticationResponse(
        string Id,
        string? Email,
        string FirstName,
        string LastName,
        string Token,
        int ExpiresIn

        );
    
}
