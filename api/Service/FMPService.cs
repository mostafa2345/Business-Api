using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Newtonsoft.Json;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public FMPService(HttpClient httpClient,IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }
        public async Task<Stock> FindStockBySymbol(string symbol)
        {
            try{
                var result = await httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={configuration["FMPKey"]}");
                if(result.IsSuccessStatusCode){
                    var content=await  result.Content.ReadAsStringAsync();
                    var Task=JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var Stock=Task[0];
                    if(Stock != null){
                        return Stock.TostockDtoFromFMP();
                    }
                    return null;
                }
                return null;
            }
            catch(Exception e){
                Console.Write(e);
                return null; 
            }
        }
    }
}