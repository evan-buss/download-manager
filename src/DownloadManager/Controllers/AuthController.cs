using DownloadManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DownloadManager.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> LogIn([FromBody] LoginRequest model)
        {
            var tokens = await _service.LogIn(model).ConfigureAwait(false);
            if (tokens == null)
            {
                return Unauthorized();
            }

            return Ok(tokens);
        }

        [HttpPost("register")]
        public async Task<ActionResult<TokenResponse>> Register([FromBody] LoginRequest model)
        {
            var tokens = await _service.RegisterUser(model).ConfigureAwait(false);
            if (tokens == null)
            {
                return BadRequest();
            }

            return Ok(tokens);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            var userId = User.FindFirst("id");
            var success = await _service.LogOut(long.Parse(userId.Value)).ConfigureAwait(false);
            return success ? (StatusCodeResult)Ok() : BadRequest();
        }
    }
}