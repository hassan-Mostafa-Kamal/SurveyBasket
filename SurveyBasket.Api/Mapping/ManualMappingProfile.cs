using SurveyBasket.Api.Contarcts.DTOs;

namespace SurveyBasket.Api.Mapping
{
    public static class ManualMappingProfile
    {
        public static PollDto MaptoPollDto (this Poll poll)
        {
            return new PollDto
            {
                //Id = poll.Id,
                Summary = poll.Summary,
                Titel = poll.Titel,
                EndsAt = poll.EndsAt,
               // IsPublished = poll.IsPublished,
                StartsAt = poll.StartsAt
            };
        }


        public static IEnumerable<PollDto> MaptoPollDto(this IEnumerable<Poll> poll)
        {
            return poll.Select(MaptoPollDto);
            
        }



        public static Poll MaptoPoll(this CreateOrUpdatePollDto poll)
        {
            return new Poll
            {
                Summary = poll.Summary,
                Titel = poll.Titel,
                StartsAt = poll.StartsAt,
                EndsAt = poll.EndsAt,
                IsPublished = poll.IsPublished,
            };
        }
    }
}
