using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Business.Abstract;
using StockMarket.Entities.Concrete;

namespace StockMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockDataController : ControllerBase
    {
        private readonly IStockDataService _stockDataService;

        public StockDataController(IStockDataService stockDataService)
        {
            _stockDataService = stockDataService;
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetStockBySymbol(string symbol)
        {
            var stock = await _stockDataService.GetStockBySymbolAsync(symbol);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        [HttpGet("StockName/{name}")]
        public async Task<IActionResult> GetStockByName(string name)
        {
            var stock = await _stockDataService.GetStockByNameAsync(name);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }
        
        [Authorize("AdminOnly")]
        [HttpDelete("{symbol}")]
        public async Task<IActionResult> DeleteStock(string symbol)
        {
            var stock = await  _stockDataService.GetStockBySymbolAsync(symbol);
            if (stock == null)
                return NotFound();

            await _stockDataService.DeleteStockAsync(stock);
            return Ok("Stock başarıyla silindi.");
        }
    }
}
