namespace SurveyBasket.Api.Contracts.Validations
{
    public class LoginDtoValidator:AbstractValidator<RefreshTokenRequest>
    {
        public LoginDtoValidator()
        {
            RuleFor(e => e.Token).NotEmpty();

            RuleFor(e => e.RefreshToken).NotEmpty();

        }
    }
}
