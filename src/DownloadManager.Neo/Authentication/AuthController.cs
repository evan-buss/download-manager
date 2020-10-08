using DownloadManager.Neo.Authentication.Commands;
using DownloadManager.Neo.Authentication.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DownloadManager.Neo.Authentication
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> LogIn([FromBody] LoginCommand command)
        {
            var tokens = await _mediator.Send(command);
            if (tokens == null)
            {
                return Unauthorized();
            }

            return Ok(tokens);
        }

        [HttpPost("register")]
        public async Task<ActionResult<TokenResponse>> Register([FromBody] RegisterCommand command)
        {
            var tokens = await _mediator.Send(command);
            if (tokens == null)
            {
                return BadRequest();
            }

            return Ok(tokens);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            await Task.Delay(10);
            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            var userId = User.FindFirst("id");
            var success = await _mediator.Send(new LogoutCommand { UserId = long.Parse(userId.Value) });

            return success ? (StatusCodeResult)Ok() : BadRequest();
        }
    }
}