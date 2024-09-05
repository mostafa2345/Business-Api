using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockReqDto
    {
        [Required]
      
        [MaxLength(10, ErrorMessage = "Symbol max is 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]

        [MaxLength(10, ErrorMessage = "CompanyName max is 10 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1,1000000)]
      
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001,100)]
   
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry max is 10 characters")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000)]
        public long MarketCap { get; set; }
    }
}