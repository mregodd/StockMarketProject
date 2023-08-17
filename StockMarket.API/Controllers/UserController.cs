using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Business.Abstract;
using StockMarket.Business.Concrete;
using StockMarket.Entities.Concrete;
using System.Threading.Tasks;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBalanceService _balanceService;
        private readonly IPortfolioService _portfolioService;

        public UserController(IUserService userService, IBalanceService balanceService,IPortfolioService portfolioService)
        {
            _userService = userService;
            _balanceService = balanceService;
            _portfolioService = portfolioService;
        }

        [HttpGet("{userId}/balance")]
        public IActionResult GetUserBalance(int userId)
        {
            var user = _userService.GetUserById(userId).Result;

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var balance = _balanceService.GetUserBalance(userId);

            return Ok(new { Balance = balance.Balance });
        }
        [Authorize("AdminOnly")]
        [HttpPost("{userId}/addbalance")]
        public IActionResult AddUserBalance(int userId, [FromBody] AddBalanceModel model)
        {
            var user = _userService.GetUserById(userId).Result;

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            _balanceService.AddUserBalance(userId, model.Amount);

            return Ok("Bakiye başarıyla eklendi.");
        }

        [Authorize("AdminOnly")]
        [HttpPost("{userId}/subtractbalance")]
        public IActionResult SubtractUserBalance(int userId, [FromBody] AddBalanceModel model)
        {
            var user = _userService.GetUserById(userId).Result;

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            _balanceService.SubtractUserBalance(userId, model.Amount);

            return Ok("Bakiyeden para düşürüldü.");
        }

        [Authorize("AdminOnly")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Geçersiz model.");
            }

            await _userService.CreateUser(model.Username, model.Password);

            return Ok("Kullanıcı başarıyla oluşturuldu.");
        }

        [Authorize("AdminOnly")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserModel model)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            user.UserName = model.Username;
            // Diğer özellikleri de güncelleyebilirsiniz

            await _userService.UpdateUser(user);

            return Ok("Kullanıcı bilgileri güncellendi.");
        }

        [Authorize("AdminOnly")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            await _userService.DeleteUser(userId);

            return Ok("Kullanıcı başarıyla silindi.");
        }
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return Ok(user);
        }
        [HttpGet("portfoliobyid/{userId}")]
        public async Task<IActionResult> GetPortfolioByUserId(int userId)
        {
            try
            {
                var portfolio = await _portfolioService.GetPortfolioByUserIdAsync(userId);
                return Ok(portfolio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("AdminOnly")]
        [HttpDelete("deleteportfolio/{portfolioId}")]
        public async Task<IActionResult> DeletePortfolio(int portfolioId)
        {
            try
            {
                await _portfolioService.DeletePortfolioAsync(portfolioId);
                return Ok("Portföy başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class CreateUserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserModel
    {
        public string Username { get; set; }
    }

}

