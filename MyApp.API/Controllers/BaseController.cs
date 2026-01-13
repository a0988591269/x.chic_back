using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commons.Results;
using IResult = MyApp.Application.Commons.Results.IResult;

namespace MyApp.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// 處理泛型 Result (通常用於查詢 Query 或有回傳值的 Command)
        /// </summary>
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            // 如果是 null，這屬於嚴重錯誤 (不該發生)
            if (result == null) return StatusCode(500, "Result cannot be null.");

            // 處理成功系列 (2xx)
            if (result.IsSuccess)
            {
                return result.Status switch
                {
                    // 有嚴格要求 header 裡要有 Location 時，在特定的 Controller Action 裡手動寫 Created / CreatedAtAction 即可
                    ResultStatus.Created => StatusCode(201, result.Data),
                    ResultStatus.Accepted => Accepted(result.Data), // (通常用於非同步處理中)
                    ResultStatus.NoContent => NoContent(),
                    _ => Ok(result.Data)    // 預設 200 OK
                };
            }

            // 失敗：轉交給錯誤處理邏輯
            return HandleErrorResult(result);
        }

        /// <summary>
        /// 處理非泛型 Result (通常用於無回傳值的 Command，如 Delete/Update)
        /// </summary>
        protected ActionResult HandleResult(Result result)
        {
            if (result == null) return StatusCode(500, "Result cannot be null.");

            if (result.IsSuccess)
            {
                return result.Status switch
                {
                    // 有嚴格要求 header 裡要有 Location 時，在特定的 Controller Action 裡手動寫 Created / CreatedAtAction 即可
                    ResultStatus.Created => StatusCode(201),    // (無回傳值版，較少見但合規)
                    ResultStatus.Accepted => Accepted(),
                    ResultStatus.NoContent => NoContent(),
                    _ => Ok()
                };
            }

            return HandleErrorResult(result);
        }

        /// <summary>
        /// 統一錯誤處理邏輯 (Switch Expression)
        /// </summary>
        private ActionResult HandleErrorResult(IResult result)
        {
            // 這裡回傳的物件格式 { error = ... } 可以根據前端需求調整
            // 也可以改用 ProblemDetails (RFC 7807) 標準格式

            return result.Status switch
            {
                // 400 Bad Request (邏輯錯誤)
                ResultStatus.Error => BadRequest(new { error = result.Error }),

                // 400/422 Validation Error (驗證錯誤，回傳陣列)
                ResultStatus.Invalid => BadRequest(new { errors = result.ValidationErrors }),

                // 404 Not Found
                ResultStatus.NotFound => NotFound(new { error = result.Error }),

                // 401 Unauthorized (未登入)
                ResultStatus.Unauthorized => Unauthorized(new { error = result.Error }),

                // 403 Forbidden (無權限) - 注意：使用 Forbid() 有時會觸發轉址，API 建議用 StatusCode(403)
                ResultStatus.Forbidden => StatusCode(403, new { error = result.Error }),

                // 409 Conflict (衝突/重複)
                ResultStatus.Conflict => Conflict(new { error = result.Error }),

                // 500 Critical Error
                ResultStatus.CriticalError => StatusCode(500, new { error = result.Error }),

                // 預設防呆 (500)
                _ => StatusCode(500, "An unexpected error occurred.")
            };
        }
    }
}
