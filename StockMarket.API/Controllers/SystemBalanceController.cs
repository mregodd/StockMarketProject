using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Business.Abstract;

namespace StockMarket.API.Controllers
{
    [Route("api/systembalance")]
    [ApiController]
    [Authorize]
    public class SystemBalanceController : ControllerBase
    {
        private readonly ISystemBalanceService _systemBalanceService;

        public SystemBalanceController(ISystemBalanceService systemBalanceService)
        {
            _systemBalanceService = systemBalanceService;
        }
            [Authorize("AdminOnly")]
            [HttpPost("updatesystembalance")]

            public IActionResult UpdateSystemBalance([FromBody] decimal newBalance)
            {
                _systemBalanceService.UpdateSystemBalance(newBalance);
                return Ok("Sistem bakiyesi güncellendi.");
            }

            [HttpGet]
            public IActionResult GetSystemBalance()
            {
                var systemBalance = _systemBalanceService.GetSystemBalance();
                return Ok(new { SystemBalance = systemBalance });
            }

    }
}
    

