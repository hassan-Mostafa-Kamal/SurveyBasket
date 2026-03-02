namespace SurveyBasket.Api.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
