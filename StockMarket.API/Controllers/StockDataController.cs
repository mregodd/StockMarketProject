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
        public IActionResult GetStockBySymbol(string symbol)
        {
            var stock = _stockDataService.GetStockBySymbol(symbol);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        [HttpGet("StockName/{name}")]
        public IActionResult GetStockByName(string name)
        {
            var stock = _stockDataService.GetStockByName(name);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }
        
        [Authorize("AdminOnly")]
        [HttpDelete("{symbol}")]
        public IActionResult DeleteStock(string symbol)
        {
            var stock = _stockDataService.GetStockBySymbol(symbol);
            if (stock == null)
                return NotFound();

            _stockDataService.DeleteStock(stock);
            return Ok("Stock başarıyla silindi.");
        }
    }
}
