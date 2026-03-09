using Mapster;
using SurveyBasket.Api.Contarcts.DTOs;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers

{
    [Route("api/[controller]")]  //BaseUrl/api/Polls
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;

        public IValidator<CreateOrUpdatePollDto> _Validator { get; }

        public PollsController(IPollService pollService ,IValidator<CreateOrUpdatePollDto> validator)
        {
            _pollService = pollService;
            _Validator = validator;
        }

      
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var polls =await  _pollService.GetAllAsync(cancellationToken);
            var pollsDto = polls.Adapt<IEnumerable<PollDto>>();

            //return Ok(polls.MaptoPollDto());
            return Ok(pollsDto);

        }




        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id, CancellationToken cancellationToken)
        {
            var poll = await  _pollService.GetAsync(id, cancellationToken);
            if (poll == null)
            {
                return NotFound();
            }
            //  return Ok(poll.MaptoPollDto());
          var  pollDto = poll.Adapt<PollDto>();
            return Ok(pollDto);

        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] CreateOrUpdatePollDto CreatePollDto,CancellationToken cancellationToken) {

            //var validationResult = _Validator.Validate(CreatePollDto);
            //if (!validationResult.IsValid)
            //{
            //    var moelState = new ModelStateDictionary();
            //    validationResult.Errors.ForEach(x => moelState.AddModelError(x.PropertyName, x.ErrorMessage));
            //    return ValidationProblem(moelState);
            //}



            // var newPoll = _pollService.Add(pollDto.MaptoPoll());
            var newPoll = await  _pollService.AddAsync(CreatePollDto.Adapt<Poll>(), cancellationToken);
            return CreatedAtAction(nameof(Get),new {id = newPoll.Id},newPoll); //201
        
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateOrUpdatePollDto updatePollDto,CancellationToken cancellationToken)
        {
            //var isUpdated = _pollService.Update(id, pollDto.MaptoPoll());
            var isUpdated =await _pollService.UpdateAsync(id, updatePollDto.Adapt<Poll>(),cancellationToken);
            if (!isUpdated)
                return NotFound();
            return NoContent(); // 204

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var isDeleted =await _pollService.DeleteAsync(id,cancellationToken);
            if (!isDeleted)
                return NotFound();
            return NoContent(); // 204

        }

        [HttpPut("{id}/togglePublish")]

        public async Task<IActionResult> TogglePublish(int id,CancellationToken cancellationToken = default)
        {
          var  isUpdated = await _pollService.TogglePublishStatusAsync(id);
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent(); // 204
        }
    }
}
