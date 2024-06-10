
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeleAfiaPersonal.Application.Authentication.Command.Register;

namespace TeleAfiaPersonal.Api.Controllers.AuthenticationControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "User registered successfully." });
        }
    }
}
