using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Users.AdminSignup;
using MyApp.Application.Features.Users.GetAllUsers;

namespace MyApp.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost("getAllUsers")]
        public async Task<ActionResult> GetAllUsers([FromBody] GetAllUsersQuery command)
        {
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }

        [HttpPost("signup")]
        public async Task<ActionResult> AdminSignup([FromBody] AdminSignupCommand command)
        {
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }
    }
}
