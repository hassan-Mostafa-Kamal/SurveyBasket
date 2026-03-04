using SurveyBasket.Api.DTOs;

namespace SurveyBasket.Api.Mapping
{
    public static class ManualMappingProfile
    {
        public static PollDto MaptoPollDto (this Poll poll)
        {
            return new PollDto
            {
                Id = poll.Id,
                Description = poll.Description,
                Titel = poll.Titel,
            };
        }


        public static IEnumerable<PollDto> MaptoPollDto(this IEnumerable<Poll> poll)
        {
            return poll.Select(MaptoPollDto);
            
        }



        public static Poll MaptoPoll(this PollDto poll)
        {
            return new Poll
            {
                Description = poll.Description,
                Titel = poll.Titel,
            };
        }
    }
}
