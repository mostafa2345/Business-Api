using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>>GetAllAsync(QueryObject queryObject);
        Task<Stock?>GetByIdAsync(int id);
        Task<Stock?>GetBySymbolAsync(string symbol);
        Task<Stock>CreateStockAsync(Stock stockModel);

        Task<Stock?>UpdateStockAsync(int id,UpdateStockReqDto stockDto);

        Task<Stock>DeleteStockAsync(int id);
        Task<bool> StockExist(int id);
    }
}