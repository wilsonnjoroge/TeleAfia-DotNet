using Microsoft.AspNetCore.Mvc;
using MediatR;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;
using AutoMapper;
using TeleAfiaPersonal.Application.Authentication.Command.ForgotPassword;

namespace TeleAfiaPersonal.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ForgotPasswordController(IMediator mediator, IMapper mapper )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new { Message = "Invalid email address." });
            }

            var query = new ForgotPasswordCommand { ForgotPasswordRequest = request };
            var response = await _mediator.Send(query);

            var mappedResponse = _mapper.Map<ForgotPasswordResponse>(response);
            return Ok(mappedResponse);
        }
    }
}
