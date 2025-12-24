using Microsoft.AspNetCore.Identity;

namespace MyApp.Shared.Helpers
{
    public static class PasswordHasher
    {
        private static readonly PasswordHasher<object> _hasher = new();

        // 註冊 / 修改密碼用
        public static string Hash(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        // 登入驗證用
        public static bool Verify(string password, string hashedPassword)
        {
            var result = _hasher.VerifyHashedPassword(
                null!,
                hashedPassword,
                password
            );

            return result == PasswordVerificationResult.Success
                || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
