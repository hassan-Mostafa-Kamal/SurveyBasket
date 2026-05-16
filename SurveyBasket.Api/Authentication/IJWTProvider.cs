namespace SurveyBasket.Api.Authentication
{
    public interface IJWTProvider
    {
        (string token ,int expiresIn) GenerateToken(ApplicationUser user);

       
        string? ValidateToken (string token);    //validate the current token and get user Id from it  

    }
}
