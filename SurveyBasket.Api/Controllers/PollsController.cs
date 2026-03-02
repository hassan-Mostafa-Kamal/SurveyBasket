using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers

{
    [Route("api/[controller]")]  //BaseUrl/api/Polls
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;

        public PollsController(IPollService pollService)
        {
            _pollService = pollService;
        }

      
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var polls = _pollService.GetAll();

            return Ok(polls);

        }




        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var poll = _pollService.Get(id);
            if (poll == null)
            {
                return NotFound();
            }
            return Ok(poll);

        }

        [HttpPost("")]
        public IActionResult Add(Poll poll) { 
        
           var newPoll = _pollService.Add(poll);
            return CreatedAtAction(nameof(Get),new {id = newPoll.Id},newPoll); //201
        
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Poll poll)
        {
           var isUpdated = _pollService.Update(id,poll);
            if (!isUpdated)
                return NotFound();
            return NoContent(); // 204

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _pollService.Delete(id);
            if (!isDeleted)
                return NotFound();
            return NoContent(); // 204

        }
    }
}
