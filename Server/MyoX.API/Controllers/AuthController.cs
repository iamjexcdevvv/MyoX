using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyoX.Application.Abstraction.Command;
using MyoX.Application.DTO;
using MyoX.Application.Features.Authentication.Login;
using MyoX.Application.Features.Authentication.Register;

namespace MyoX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICommandHandler<RegisterCommand> _registerHandler;
        private readonly ICommandHandler<LoginCommand, AuthTokenDTO> _loginHandler;
        public AuthController(ICommandHandler<RegisterCommand> registerHandler, ICommandHandler<LoginCommand, AuthTokenDTO> loginHandler)
        {
            _registerHandler = registerHandler;
            _loginHandler = loginHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            var command = new LoginCommand(request);
            var result = await _loginHandler.Handle(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO request)
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
