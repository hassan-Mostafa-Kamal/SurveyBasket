
using Microsoft.AspNetCore.Http.HttpResults;

namespace SurveyBasket.Api.Services
{
    public class PollService : IPollService
    {

        private static  readonly List<Poll> _polls = [

           new Poll {
                Id = 1,
                Titel = "poll 1",
                Summary = "My First Poll"
            }

           ];//new List<Polls>();
        public IEnumerable<Poll> GetAll()
        {
            return _polls;
        }
        public Poll? Get(int id)
        {
            return _polls.FirstOrDefault(p => p.Id == id);
        }

        public Poll Add(Poll poll)
        {
            poll.Id = _polls.Count + 1;
            _polls.Add(poll);
            return poll;

        }

        public bool Update(int id, Poll poll)
        {
            var currentPoll = Get(id);
            if (currentPoll == null)
            {
                return false;
            }
            currentPoll.Summary = poll.Summary;
            currentPoll.Titel = poll.Titel;
            return true;

        }

        public bool Delete(int id)
        {
            var Poll = Get(id);
            if (Poll == null)
            {
                return false;
            }
            _polls.Remove(Poll);
            return true;

        }
    }
}
