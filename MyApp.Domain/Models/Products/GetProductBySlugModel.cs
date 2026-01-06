using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Models.Products
{
    public class GetProductBySlugModel
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

        /// <summary>
        /// 主鍵
        /// </summary>
        public long ProductVariantId { get; set; }

        /// <summary>
        /// SKU 編碼，唯一
        /// </summary>
        public string Sku { get; set; } = string.Empty;

        /// <summary>
        /// 銷售價格
        /// </summary>
        // Price precision handled in FluentConfig
        public decimal Price { get; set; }

        /// <summary>
        /// 折扣價格
        /// </summary>
        public decimal? DiscountPrice { get; set; }   // 如果有折扣

        /// <summary>
        /// 庫存數量
        /// </summary>
        public int StockQty { get; set; } = 0;

        /// <summary>
        /// 圖片 URL
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
    }
}
