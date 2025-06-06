using Microsoft.AspNetCore.Mvc;
using LabApp.Dtos;
using LabApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace LabApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {

            var success = await _authService.RegisterAsync(dto);
            if (!success)
                return BadRequest("Użytkownik o podanej nazwie już istnieje.");

            return Ok("Zarejestrowano pomyślnie.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            if (response == null)
                return Unauthorized("Nieprawidłowy login lub hasło.");

            Response.Cookies.Append("jwt", response.Token, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict });
            return Ok(response);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetUser()
        {
            var username = User.Identity?.Name;
            return Ok(new { message = $"Zalogowano jako {username}" });
        }
    }
}
