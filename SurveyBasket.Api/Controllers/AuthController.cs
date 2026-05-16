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
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto,CancellationToken cancellationToken = default)
        {

            var authResulte = await _authService.GetTokenAsync(loginDto.Email, loginDto.Password, cancellationToken);
            if (authResulte == null)
                return BadRequest("User Email or Password is Invalid");
            return Ok(authResulte);

        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {

            var authResulte = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
            if (authResulte == null)
                return BadRequest("Token is Invalid");
            return Ok(authResulte);

        }
        [HttpPost("revoke-refresh-token")]
        public async Task<IActionResult> RevoleRefreshAsync([FromBody]RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {

            var isRevoke = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

            return isRevoke ? Ok() : BadRequest("Operation Failed");

        }
    }
}
