using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Users.Login;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyApp.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Auth")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;

        public AuthController(ILogger<AuthController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginDto>> Login([FromForm] LoginQuery query)
        {
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);
        }
    }
}
