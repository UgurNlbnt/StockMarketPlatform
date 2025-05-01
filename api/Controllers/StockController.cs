using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
      private readonly ApplicationDbContext _context;

      public StockController(ApplicationDbContext context)
      {
          _context = context;
      }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stock = await _context.Stock.ToListAsync();

            var stockDto = stock.Select(s => s.toStockDto());

            return Ok(stock);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.toStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.toStockDto());
        }

         [HttpPut]
         [Route("{id}")]
            public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
            {
                var stockModel = await _context.Stock.FirstOrDefaultAsync(x =>x.Id==id);
                if (stockModel == null)
                {
                    return NotFound();
                }
    
                stockModel.Symbol = updateDto.Symbol;
                stockModel.CompanyName = updateDto.CompanyName;
                stockModel.Purchase = updateDto.Purchase;
                stockModel.LastDiv = updateDto.LastDiv;
                stockModel.Industry = updateDto.Industry;
                stockModel.MarketCap = updateDto.MarketCap;
    
                await _context.SaveChangesAsync();
                return Ok(stockModel.toStockDto());
            }

            [HttpDelete]
            [Route("{id}")]
            public async Task<IActionResult> Delete([FromRoute] int id)
            {
                var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
                if (stockModel == null)
                {
                    return NotFound();
                }
    
                _context.Stock.Remove(stockModel);
                await _context.SaveChangesAsync();
                return NoContent();
            }
    }
}