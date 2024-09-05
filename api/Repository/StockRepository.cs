using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext dbContext;

        public StockRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = dbContext.stocks.Include(c => c.comments).ThenInclude(z=>z.appUser).AsQueryable();

            if (!string.IsNullOrEmpty(queryObject.CompanyName))
            {
                stocks = stocks.Where(x => x.CompanyName.Contains(queryObject.CompanyName));


            }

            if (!string.IsNullOrEmpty(queryObject.Symbol))
            {
                stocks = stocks.Where(x => x.Symbol.Contains(queryObject.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(queryObject.SortBy)){
                {
                    if(queryObject.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase)){
                            stocks=queryObject.IsDecsending ?stocks.OrderByDescending(s=>s.Symbol):stocks.OrderBy(s=>s.Symbol);
                    }
                }
            }

//pagination

                var skipNumber=(queryObject.PageNumber-1)*queryObject.PageSize;






            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
//end pagination
        }
        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await dbContext.stocks.Include(c => c.comments).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Stock> CreateStockAsync(Stock stockModel)
        {
            await dbContext.stocks.AddAsync(stockModel);
            await dbContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockReqDto stockDto)
        {
            var stockModel = await dbContext.stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            stockModel.Symbol = stockDto.Symbol;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.MarketCap = stockDto.MarketCap;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;
            await dbContext.SaveChangesAsync();
            return stockModel;
        }
        public async Task<Stock> DeleteStockAsync(int id)
        {
            var stockModel = await dbContext.stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            dbContext.stocks.Remove(stockModel);
            await dbContext.SaveChangesAsync();

            return stockModel;

        }

        public async Task<bool> StockExist(int id)
        {
            return await dbContext.stocks.AnyAsync(s => s.Id == id);
        }

        public Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return dbContext.stocks.FirstOrDefaultAsync(x=>x.Symbol == symbol);
        }
    }
}