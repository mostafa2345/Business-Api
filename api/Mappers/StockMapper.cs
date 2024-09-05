using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto TostockDto(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                CompanyName = stock.CompanyName,
                Industry = stock.Industry,
                LastDiv = stock.LastDiv,
                MarketCap = stock.MarketCap,
                Purchase = stock.Purchase,
                Symbol = stock.Symbol,
                comments = stock.comments.Select(c=>c.ToCommentDto()).ToList()
                

            };
        }

       

        public static Stock ToStockFromCreateReqDto(this CreateStockReqDto stockDto)
        {

            return new Stock
            {
                CompanyName = stockDto.CompanyName,
                Industry = stockDto.Industry,
                LastDiv = stockDto.LastDiv,
                MarketCap = stockDto.MarketCap,
                Purchase = stockDto.Purchase,
                Symbol = stockDto.Symbol
            };
        }
        public static Stock ToStockFromUpdateReqDto(this UpdateStockReqDto stockDto){
            return new Stock
            {
                CompanyName = stockDto.CompanyName,
                Industry = stockDto.Industry,
                LastDiv = stockDto.LastDiv,
                MarketCap = stockDto.MarketCap,
                Purchase = stockDto.Purchase,
                Symbol = stockDto.Symbol
            };
        }
    }
}