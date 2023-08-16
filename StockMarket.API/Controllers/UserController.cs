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
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBalanceService _balanceService;

        public UserController(IUserService userService, IBalanceService balanceService)
        {
            _userService = userService;
            _balanceService = balanceService;
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
        public IActionResult AddUserBalance(int userId, [FromBody] decimal amount)
        {
            var user = _userService.GetUserById(userId).Result;

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            _balanceService.AddUserBalance(userId, amount);

            return Ok("Bakiye başarıyla eklendi.");
        }

        [Authorize("AdminOnly")]
        [HttpPost("{userId}/subtractbalance")]
        public IActionResult SubtractUserBalance(int userId, [FromBody] decimal amount)
        {
            var user = _userService.GetUserById(userId).Result;

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            _balanceService.SubtractUserBalance(userId, amount);

            return Ok("Bakiyeden para düşürüldü.");
        }

        [Authorize("AdminOnly")]
        [HttpPost]
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
