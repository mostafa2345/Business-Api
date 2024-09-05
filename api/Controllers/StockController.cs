using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IStockRepository stockRepository;

        public StockController(AppDbContext dbContext, IStockRepository stockRepository)
        {
            this.dbContext = dbContext;
            this.stockRepository = stockRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await stockRepository.GetAllAsync(queryObject);
            var result = stock.Select(s => s.TostockDto()).ToList();
            return Ok(result);

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await stockRepository.GetByIdAsync(id);

            if (item != null)
            {
                return Ok(item.TostockDto());

            }

            return NotFound();



        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockReqDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockFromCreateReqDto();

            await stockRepository.CreateStockAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.TostockDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockReqDto updateStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await stockRepository.UpdateStockAsync(id, updateStock);
            if (stockModel == null)
            {
                return NotFound();
            }



            return Ok(stockModel.TostockDto());


        }
        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await stockRepository.DeleteStockAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();



        }

    }
}