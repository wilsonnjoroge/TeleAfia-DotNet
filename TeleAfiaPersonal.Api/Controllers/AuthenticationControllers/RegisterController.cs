using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeleAfiaPersonal.Application.Authentication.Command.Register;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;

namespace TeleAfiaPersonal.Api.Controllers.AuthenticationControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var response = await _mediator.Send(command);
           var mappedResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(mappedResponse);
        }
    }
}
