using Mapster;
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
            var pollsDto = polls.Adapt<IEnumerable<PollDto>>();

            //return Ok(polls.MaptoPollDto());
            return Ok(pollsDto);

        }




        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var poll = _pollService.Get(id);
            if (poll == null)
            {
                return NotFound();
            }
            //  return Ok(poll.MaptoPollDto());
          var  pollDto = poll.Adapt<PollDto>();
            return Ok(pollDto);

        }

        [HttpPost("")]
        public IActionResult Add([FromBody] CreateOrUpdatePollDto CreatePollDto) {

            // var newPoll = _pollService.Add(pollDto.MaptoPoll());
            var newPoll = _pollService.Add(CreatePollDto.Adapt<Poll>());
            return CreatedAtAction(nameof(Get),new {id = newPoll.Id},newPoll); //201
        
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id,[FromBody] CreateOrUpdatePollDto updatePollDto)
        {
            //var isUpdated = _pollService.Update(id, pollDto.MaptoPoll());
            var isUpdated = _pollService.Update(id, updatePollDto.Adapt<Poll>());
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
