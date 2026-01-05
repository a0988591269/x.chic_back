using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyApp.Application.Commons.Results;
using MyApp.Application.Features.Users.Login;
using MyApp.Application.Features.Users.Signup;
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
        private readonly IWebHostEnvironment _env; // 注入環境設定

        public AuthController(ILogger<AuthController> logger, IMediator mediator, IWebHostEnvironment env)
        {
            _logger = logger;
            _mediator = mediator;
            _env = env;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginQuery query)
        {
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            // 寫入 HttpOnly Cookie
            SetTokenCookie(result.Data?.AccessToken ?? "");

            return Ok();
        }

        [HttpPost("signup")]
        public async Task<ActionResult> Signup([FromBody] SignupCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // 優化 4: 清除 Cookie (將過期時間設為過去)
            Response.Cookies.Delete("access_token", new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok(new { message = "已登出" });
        }

        [Authorize]
        [HttpGet("userInfo")]
        public IActionResult UserInfo()
        {
            var user = new
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                // 加上 ?. 以防 claim 不存在時報錯
                UserUuid = User.FindFirstValue("user_uuid"),
                UserName = User.FindFirstValue(ClaimTypes.Name),
                Email = User.FindFirstValue(ClaimTypes.Email),
                Tier = User.FindFirstValue("tier"),
                Roles = User.FindAll(ClaimTypes.Role).Select(x => x.Value),
                Permissions = User.FindAll("permission").Select(x => x.Value)
            };

            return Ok(user);
        }

        // 私有方法：統一管理 Cookie 設定
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                // 優化 5: Lax 模式對 UX 比較友善
                //SameSite = SameSiteMode.Lax,  // 不要用 Strict
                SameSite = SameSiteMode.None,
                // 優化 6: 如果是開發環境且沒跑 HTTPS，可以考慮放寬 (但在 .NET 8 預設都有 HTTPS)
                //Secure = _env.IsDevelopment() ? false : true,
                Secure = true,
                // 設定過期時間 (建議與 JWT exp 一致)
                Expires = DateTime.UtcNow.AddHours(2)
            };

            Response.Cookies.Append("access_token", token, cookieOptions);
        }
    }
}
