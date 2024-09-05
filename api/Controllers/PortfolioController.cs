using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IStockRepository stockRepository;
        private readonly IPortfolioRepository portfolioRepository;
 

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            this.userManager = userManager;
            this.stockRepository = stockRepository;
            this.portfolioRepository = portfolioRepository;
  
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
            if (username != null)
            {
                var appUser = await userManager.FindByNameAsync(username);
                if (appUser != null)
                {
                    var userPortfolio = await portfolioRepository.GetUserPortfolio(appUser);

                    return Ok(userPortfolio);
                }
            }

            return BadRequest("User not found");

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePortfolio(string Symbol)
        {
            var username = User.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
            var appUser = await userManager.FindByNameAsync(username);
            var stock = await stockRepository.GetBySymbolAsync(Symbol);
      
            if (stock == null) return BadRequest("stock not found");

            var userPortfolio = await portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == Symbol.ToLower())) return BadRequest("cant add same stock twice");
            var portfolio = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };
            await portfolioRepository.CreatePortfolio(portfolio);
            if (portfolio == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string Symbol)
        {
            var username = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(username);
            var userPortfolio = await portfolioRepository.GetUserPortfolio(appUser);
            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == Symbol.ToLower()).ToList();

            if (filteredStock.Count() == 1)
            {
                await portfolioRepository.DeletePortfolio(appUser, Symbol);

            }
            else
            {
                return BadRequest("Stock not in your portfolio");
            }
            return Ok();

        }
    }


}