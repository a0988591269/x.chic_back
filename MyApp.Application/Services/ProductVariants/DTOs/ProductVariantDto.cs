using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Services.ProductVariants.DTOs
{
    public class ProductVariantDto
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        public long ProductVariantId { get; set; }

        /// <summary>
        /// 外部主鍵 (FK)
        /// </summary>
        public long ProductId { get; set; }

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
        /// UPC / EAN
        /// </summary>
        public string? Barcode { get; set; }

        /// <summary>
        /// 是否可銷售
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
