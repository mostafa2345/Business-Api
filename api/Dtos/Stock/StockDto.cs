using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;


        public decimal Purchase { get; set; }


        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }
        //comment was here  and i dont want to show it  

        public List<CommentDto> comments { get; set; }
    }
}