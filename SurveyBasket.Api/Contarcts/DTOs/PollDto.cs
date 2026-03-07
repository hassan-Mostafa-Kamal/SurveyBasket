namespace SurveyBasket.Api.Contarcts.DTOs
{
    public class PollDto
    {
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
