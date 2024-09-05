using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
  
    {
        private readonly AppDbContext appDbContext;

        public PortfolioRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            await appDbContext.portfolios.AddAsync(portfolio);
            await appDbContext.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser appUser, string Symbol)
        {
          var portfolio = await appDbContext.portfolios.FirstOrDefaultAsync(x=>x.AppUserId == appUser.Id&&x.Stock.Symbol==Symbol);
        
        if(portfolio == null){
            return null;
        }
         appDbContext.portfolios.Remove(portfolio);
         await appDbContext.SaveChangesAsync();
         return portfolio;

        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser appUser)
        {
            return await appDbContext.portfolios.Where(u=>u.AppUserId==appUser.Id).Select(stock=> new Stock{
                    Id=stock.StockId,
                    Symbol=stock.Stock.Symbol,
                    CompanyName=stock.Stock.CompanyName,
                    Purchase=stock.Stock.Purchase,
                    LastDiv=stock.Stock.LastDiv,
                    Industry=stock.Stock.Industry,
                    MarketCap=stock.Stock.MarketCap,
        

            }).ToListAsync();
        }
    }
}