using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Users.Login;
using MyApp.Application.Features.Users.Signup;
using System.Security.Claims;

namespace MyApp.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Auth")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
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

            if (result.IsSuccess)
            {
                // 寫入 HttpOnly Cookie
                SetTokenCookie(result.Data?.AccessToken ?? "");
            }

            return HandleResult(result);
        }

        [HttpPost("signup")]
        public async Task<ActionResult> Signup([FromBody] SignupCommand command)
        {
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // 清除 Cookie (將過期時間設為過去)
            Response.Cookies.Delete("access_token", new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return NoContent();
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
                // 安全核心：讓 JavaScript (包含惡意腳本) 讀不到 Cookie，防 XSS
                HttpOnly = true,
                // 正式環境 (HTTPS) -> 必須為 true
                // 開發環境 (HTTP)  -> 必須為 false (否則瀏覽器會拒收)
                Secure = !_env.IsDevelopment(),
                // 使用 Lax (預設) 或 Strict 即可，不要設為 None (不安全的Cors)
                SameSite = SameSiteMode.Lax,
                // 設定過期時間 (建議與 JWT exp 一致)
                Expires = DateTime.UtcNow.AddHours(2)
            };

            Response.Cookies.Append("access_token", token, cookieOptions);
        }
    }
}
