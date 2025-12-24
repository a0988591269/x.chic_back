using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.JWT
{
    public class JwtOptions
    {
        /// <summary>
        /// 用來簽章的密鑰
        /// </summary>
        public string Secret { get; set; } = string.Empty;

        /// <summary>
        /// 發行者
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// 受眾
        /// </summary>
        public string Audience { get; set; } = string.Empty;
    }
}
