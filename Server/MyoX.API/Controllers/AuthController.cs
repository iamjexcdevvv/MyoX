using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyoX.Application.Abstraction.Command;
using MyoX.Application.DTO;
using MyoX.Application.Features.Authentication.Register;

namespace MyoX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICommandHandler<RegisterCommand> _registerHandler;
        public AuthController(ICommandHandler<RegisterCommand> registerHandler)
        {
            _registerHandler = registerHandler;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            var command = new RegisterCommand(request);
            var result = await _registerHandler.Handle(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
