using SurveyBasket.Api.DTOs;
using SurveyBasket.Api.Mapping;
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

            return Ok(polls.MaptoPollDto());

        }




        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var poll = _pollService.Get(id);
            if (poll == null)
            {
                return NotFound();
            }
            return Ok(poll.MaptoPollDto());

        }

        [HttpPost("")]
        public IActionResult Add([FromBody] PollDto pollDto) { 
        
           var newPoll = _pollService.Add(pollDto.MaptoPoll());
            return CreatedAtAction(nameof(Get),new {id = newPoll.Id},newPoll); //201
        
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id,[FromBody] PollDto pollDto)
        {
           var isUpdated = _pollService.Update(id, pollDto.MaptoPoll());
            if (!isUpdated)
                return NotFound();
            return NoContent(); // 204

        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _pollService.Delete(id);
            if (!isDeleted)
                return NotFound();
            return NoContent(); // 204

        }
    }
}
