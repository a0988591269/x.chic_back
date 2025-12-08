using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commons.Results
{
    // 定義狀態列舉
    public enum ResultStatus
    {
        Success,
        Error,      // 邏輯錯誤
        NotFound,   // 找不到
        Forbidden,  // 無權限
        Invalid     // 驗證錯誤
    }

    public class Result
    {
        // 是否成功
        public bool IsSuccess { get; }

        // 錯誤訊息 (如果有的話)
        public string Error { get; }

        // 錯誤類型 (用於決定 HTTP Status Code)
        public ResultStatus Status { get; }

        // 驗證錯誤列表 (用於 400 Validation Errors)
        public IEnumerable<string> ValidationErrors { get; }

        // 建構子 (protected，強制使用靜態工廠方法)
        protected Result(bool isSuccess, ResultStatus status, string error)
        {
            IsSuccess = isSuccess;
            Status = status;
            Error = error;
            ValidationErrors = new List<string>();
        }

        protected Result(bool isSuccess, ResultStatus status, IEnumerable<string> validationErrors)
        {
            IsSuccess = isSuccess;
            Status = status;
            Error = "Validation Failed";
            ValidationErrors = validationErrors;
        }

        // --- 靜態工廠方法 (Factory Methods) ---

        // 成功
        public static Result Success()
            => new(true, ResultStatus.Success, string.Empty);

        // 一般失敗 (400 Bad Request)
        public static Result Failure(string error)
            => new(false, ResultStatus.Error, error);

        // 找不到資源 (404 Not Found)
        public static Result NotFound(string error = "Resource not found")
            => new(false, ResultStatus.NotFound, error);

        // 禁止存取 (403 Forbidden)
        public static Result Forbidden(string error = "Access denied")
            => new(false, ResultStatus.Forbidden, error);

        // 驗證失敗 (422/400 Validation Error)
        public static Result Invalid(IEnumerable<string> errors)
            => new(false, ResultStatus.Invalid, errors);
    }

    public class Result<T> : Result
    {
        // 回傳的資料
        public T? Data { get; }

        // 建構子
        protected Result(T? data, bool isSuccess, ResultStatus status, string error)
            : base(isSuccess, status, error)
        {
            Data = data;
        }

        protected Result(bool isSuccess, ResultStatus status, IEnumerable<string> validationErrors)
            : base(isSuccess, status, validationErrors)
        {
            Data = default;
        }

        // --- 靜態工廠方法 ---

        // 成功並回傳資料
        public static Result<T> Success(T data)
            => new(data, true, ResultStatus.Success, string.Empty);

        // 各種失敗狀況 (使用 new 關鍵字隱藏父類別方法以改變回傳型別)

        public new static Result<T> Failure(string error)
            => new(default, false, ResultStatus.Error, error);

        public new static Result<T> NotFound(string error = "Resource not found")
            => new(default, false, ResultStatus.NotFound, error);

        public new static Result<T> Forbidden(string error = "Access denied")
            => new(default, false, ResultStatus.Forbidden, error);

        public new static Result<T> Invalid(IEnumerable<string> errors)
            => new(false, ResultStatus.Invalid, errors);
    }
}
