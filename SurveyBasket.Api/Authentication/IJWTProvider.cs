namespace SurveyBasket.Api.Authentication
{
    public interface IJWTProvider
    {
        (string token ,int expiresIn) GenerateToken(ApplicationUser user);

    }
}
