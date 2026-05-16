namespace SurveyBasket.Api.Contarcts.DTOs
{
    public record PollDto
    {
       // public int Id { get; init; }
        public string Titel { get; init; } = string.Empty;
        public string Summary { get; init; } = string.Empty;
      //  public bool IsPublished { get; init; }
        public DateOnly StartsAt { get; init; }
        public DateOnly EndsAt { get; init; }
    }
}
