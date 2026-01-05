using Microsoft.AspNetCore.Identity;
using MyApp.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<object> _hasher = new();

        // 註冊 / 修改密碼用
        public string Hash(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        // 登入驗證用
        public bool Verify(string password, string hashedPassword)
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
