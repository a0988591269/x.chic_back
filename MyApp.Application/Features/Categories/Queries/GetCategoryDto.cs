using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.Features.Categories.Queries
{
    public class GetCategoryDto
    {
        /// <summary>
        /// 分類主鍵
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 顯示用名稱（中文或其他）
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// 顯示用名稱（英文或其他）
        /// </summary>
        public string CategoryEngName { get; set; } = string.Empty;

        /// <summary>
        /// 分類描述 / SEO 用
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Slug，用於 URL
        /// </summary>
        public string Slug { get; set; } = string.Empty;
    }
}
