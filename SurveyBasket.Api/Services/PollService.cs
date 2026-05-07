using SurveyBasket.Api.persistence;
namespace SurveyBasket.Api.Services
{
    public class PollService : IPollService
    {
        public ApplicationDbContext _context { get; }

        public PollService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken)
          {
            return await _context.polls.ToListAsync(cancellationToken);
        }
        public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.polls.FindAsync(id, cancellationToken);
        }

        public async Task<Poll> AddAsync(Poll poll , CancellationToken cancellationToken = default)
        {
            await  _context.AddAsync(poll, cancellationToken);
           await _context.SaveChangesAsync(cancellationToken);
            return poll;

        }

        public async Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken = default)
        {
            var currentPoll =await GetAsync(id, cancellationToken);
            if (currentPoll == null)
            {
                return false;
            }
            currentPoll.Summary = poll.Summary;
            currentPoll.Titel = poll.Titel;
            currentPoll.StartsAt = poll.StartsAt;
            currentPoll.EndsAt = poll.EndsAt;
           // _context.Update(currentPoll);
           await _context.SaveChangesAsync(cancellationToken);
            return true;

        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var Poll =await GetAsync(id, cancellationToken);
            if (Poll == null)
            {
                return false;
            }
            _context.Remove(Poll);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }
        public async Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
        {

            var poll = await GetAsync(id, cancellationToken);
            if (poll == null)
            {
                return false;
            }
            poll.IsPublished = !poll.IsPublished;
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }
    }
}
