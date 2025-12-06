using MyApp.Application.Services.Categories.DTOs;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Application.Services.Products.DTOs
{
    public class ProductDto
    {
        /// <summary>
        /// 商品主鍵
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 商品簡短摘要
        /// </summary>
        public string? ShortDescription { get; set; }

        /// <summary>
        /// 商品詳細說明
        /// </summary>
        public string? LongDescription { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 是否上架中
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 商品銷量
        /// 通常會做每日/每小時 Batch 更新銷量
        /// </summary>
        public int TotalSales { get; set; }

        /// <summary>
        /// 商品評分
        /// </summary>
        public float Rating { get; set; }

        /// <summary>
        /// 是否熱銷
        /// </summary>
        public bool IsHot { get; set; }

        /// <summary>
        /// 是否新品
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// 是否推薦
        /// </summary>
        public bool IsRecommended { get; set; }
    }
}
