using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeleAfiaPersonal.Application.Authentication.Command.Login;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;

namespace TeleAfiaPersonal.Api.Controllers.AuthenticationControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LoginController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginQuery request)
        {
            try
            {
                var response = await _mediator.Send(request);
                var mappedResponse = _mapper.Map<AuthenticationResponse>(response);
                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                if (ex.Message == "User not found")
                {
                    return NotFound(new { message = "User not found." });
                }
                else if (ex.Message == "Wrong Password")
                {
                    return Unauthorized(new { message = "Invalid credentials." });
                }
                else
                {
                    return StatusCode(500, new { message = "An error occurred during login." });
                }
            }
        }
    }
}
