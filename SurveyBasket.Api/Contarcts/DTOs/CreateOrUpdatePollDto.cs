namespace SurveyBasket.Api.Contarcts.DTOs
{
    public record CreateOrUpdatePollDto
    {
        public string Titel { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }
}
