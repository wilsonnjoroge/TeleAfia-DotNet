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
        public async Task<IActionResult> Login([FromBody] LoginQuery command)
        {
            try
            {
                var response = await _mediator.Send(command);

                var mappedResponse = _mapper.Map<AuthenticationResponse>(response);
                return Ok(mappedResponse);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
    }
}
