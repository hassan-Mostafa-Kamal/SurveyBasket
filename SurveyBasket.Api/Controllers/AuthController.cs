using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto,CancellationToken cancellationToken = default)
        {

            var authResulte = await _authService.GetTokenAsync(loginDto.Email, loginDto.Password, cancellationToken);
            if (authResulte == null)
                return BadRequest("User Email or Password is Invalid");
            return Ok(authResulte);

        }
    }
}
