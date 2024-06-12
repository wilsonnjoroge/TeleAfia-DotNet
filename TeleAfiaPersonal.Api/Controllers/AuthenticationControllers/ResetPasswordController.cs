using Microsoft.AspNetCore.Mvc;
using MediatR;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;
using System;
using System.Threading.Tasks;
using TeleAfiaPersonal.Application.Authentication.Command.ResetPassword;
using AutoMapper;

namespace TeleAfiaPersonal.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResetPasswordController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ResetPasswordController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return BadRequest(new { Message = "Invalid request payload." });
            }

            var command = new ResetPasswordCommand { ResetPasswordRequest = request };
            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return BadRequest(new { Message = response.Message });
            }

            var mappedResponse = _mapper.Map<ResetPasswordResponse>(response);
            return Ok(mappedResponse);
        }
    }
}
