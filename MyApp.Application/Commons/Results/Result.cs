namespace MyApp.Application.Commons.Results
{
    // 定義狀態列舉：涵蓋所有常見 API 場景
    public enum ResultStatus
    {
        Success,        // 200 OK (操作成功，有回傳值)
        Created,        // 201 Created (已建立，新增資料 (POST) 成功時)
        Accepted,       // 202 Accepted (已接受 (處理中)，非同步長任務)
        NoContent,      // 204 No Content (操作成功，無回傳值，如 Delete/Update)
        Error,          // 400 Bad Request (邏輯錯誤，如：餘額不足)
        Invalid,        // 400/422 Unprocessable Entity (驗證錯誤，如：格式不對)
        NotFound,       // 404 Not Found (資源不存在)
        Unauthorized,   // 401 Unauthorized (未登入、Token 無效)
        Forbidden,      // 403 Forbidden (已登入但無權限)
        Conflict,       // 409 Conflict (資源衝突，如：重複)
        CriticalError   // 500 Internal Server Error (系統級錯誤，通常較少用，多由 Exception 處理)
    }

    // 定義介面：讓 Middleware 或 Pipeline Behavior 方便操作
    public interface IResult
    {
        bool IsSuccess { get; }
        string Error { get; }
        ResultStatus Status { get; }
        IEnumerable<string> ValidationErrors { get; }
    }

    public class Result : IResult
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

        // --- Static Factory Methods (工廠方法) ---

        // 成功 (200 OK)
        public static Result Success()
            => new(true, ResultStatus.Success, string.Empty);

        // 已建立 (201 Created) - 用於 POST 建立成功
        public static Result Created()
            => new(true, ResultStatus.Created, string.Empty);

        // 已接受 (處理中) (202 Accepted) - 用於長任務佇列
        public static Result Accepted()
            => new(true, ResultStatus.Accepted, string.Empty);

        // 成功但沒內容 (204 No Content) - 用於 Delete/Update
        public static Result NoContent()
            => new(true, ResultStatus.NoContent, string.Empty);

        // 一般邏輯錯誤 (400 Bad Request) - 例如：庫存不足
        public static Result Failure(string error)
            => new(false, ResultStatus.Error, error);

        // 找不到 (404 Not Found)
        public static Result NotFound(string error = "Resource not found")
            => new(false, ResultStatus.NotFound, error);

        // 衝突/重複 (409 Conflict) - 🆕 Benson 專用
        public static Result Conflict(string error = "Resource already exists")
            => new(false, ResultStatus.Conflict, error);

        // 未授權/未登入 (401 Unauthorized)
        public static Result Unauthorized(string error = "Unauthorized")
            => new(false, ResultStatus.Unauthorized, error);

        // 禁止/權限不足 (403 Forbidden)
        public static Result Forbidden(string error = "Access denied")
            => new(false, ResultStatus.Forbidden, error);

        // 驗證失敗 (400/422)
        public static Result Invalid(IEnumerable<string> errors)
            => new(false, ResultStatus.Invalid, errors);

        // 嚴重錯誤 (500)
        public static Result CriticalError(string error)
            => new(false, ResultStatus.CriticalError, error);
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

        // --- Implicit Operator (隱式轉換) ---
        // 讓你不用寫 Result<UserDto>.Success(userDto)，直接 return userDto; 就好
        public static implicit operator Result<T>(T data) => Success(data);

        // --- Static Factory Methods ---

        // 成功並回傳資料 (200 OK)
        public static Result<T> Success(T data)
            => new(data, true, ResultStatus.Success, string.Empty);

        public static Result<T> Created(T data)
            => new(data, true, ResultStatus.Created, string.Empty);

        public static Result<T> Accepted(T data)
            => new(data, true, ResultStatus.Accepted, string.Empty);

        public static Result<T> NoContent(T data)
            => new(data, true, ResultStatus.NoContent, string.Empty);

        // 各種失敗狀況 (使用 new 關鍵字隱藏父類別方法以改變回傳型別)

        public new static Result<T> Failure(string error)
            => new(default, false, ResultStatus.Error, error);

        public new static Result<T> NotFound(string error = "Resource not found")
            => new(default, false, ResultStatus.NotFound, error);

        public new static Result<T> Conflict(string error = "Resource already exists")
            => new(default, false, ResultStatus.Conflict, error);

        public new static Result<T> Unauthorized(string error = "Unauthorized")
            => new(default, false, ResultStatus.Unauthorized, error);

        public new static Result<T> Forbidden(string error = "Access denied")
            => new(default, false, ResultStatus.Forbidden, error);

        public new static Result<T> Invalid(IEnumerable<string> errors)
            => new(false, ResultStatus.Invalid, errors);

        public new static Result<T> CriticalError(string error)
            => new(default, false, ResultStatus.CriticalError, error);
    }
}
