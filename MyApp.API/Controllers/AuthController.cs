using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Users.Login;
using System.Security.Claims;
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
        public async Task<ActionResult> Login([FromForm] LoginQuery query)
        {
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            // 寫入 HttpOnly Cookie
            Response.Cookies.Append("access_token", result.Data.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,          // https 才傳
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(2)
            });

            return Ok();
        }

        [Authorize]
        [HttpGet("userInfo")]
        public async Task<ActionResult> UserInfo()
        {
            var user = new
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                UserUuid = User.FindFirstValue("user_uuid"),
                UserName = User.FindFirstValue(ClaimTypes.Name),
                Email = User.FindFirstValue(ClaimTypes.Email),
                Tier = User.FindFirstValue("tier"),
                Roles = User.FindAll(ClaimTypes.Role).Select(x => x.Value),
                Permissions = User.FindAll("permission").Select(x => x.Value)
            };

            return Ok(user);
        }
    }
}
