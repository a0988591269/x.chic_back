using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Users.AdminSignup;

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

        [HttpPost("signup")]
        public async Task<ActionResult> AdminSignup([FromBody] AdminSignupCommand command)
        {
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }
    }
}
