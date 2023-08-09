using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockMarket.API.Security;
using StockMarket.Business.Abstract;
using StockMarket.Business.Concrete;
using StockMarket.DataAccess.Abstract;
using StockMarket.DataAccess.Repositories;
using StockMarket.Entities.Concrete;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPortfolioService _portfolioService;
        private readonly IBalanceService _balanceService;
        private readonly ISystemBalanceService _systemBalanceService;
        private readonly IConfiguration _configuration;


        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, IPortfolioService portfolioService, IBalanceService balanceService,ISystemBalanceService systemBalanceService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _portfolioService = portfolioService;
            _balanceService = balanceService;
            _systemBalanceService = systemBalanceService;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Username,
                    Name = "NameUser",
                    Surname = "SurnameUser",
                    City = "UserCity",
                    District = "UserDistrict",

                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Kullanıcı rolü eklemesi yapıldı
                    if (!await _userManager.IsInRoleAsync(user, "User"))
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    var portfolio = new UserPortfolio
                    {
                        StockName = "Example Stock",
                        Quantity = 100,
                        Value = 1000,
                        AppUser = user // Kullanıcı kimliğini atayın
                    };

                    _portfolioService.AddPortfolio(portfolio);

                    // Token oluşturup döndürme
                    var token = TokenHandler.CreateToken(_configuration, user);
                    // Kullanıcının bakiyesini ve portfolyosunu çekme
                    var balance = _balanceService.GetUserBalance(user.Id); // Bakiye çekimi
                    var userPortfolio = _portfolioService.GetPortfolioByUserId(user.Id); // Portfolyo çekimi

                    // Token'i kullanarak bir işlem gerçekleştirebilirsiniz
                    return Ok(new
                    {
                        Message = "Kullanıcı kaydı başarıyla oluşturuldu.",
                        Token = token.AccessToken,
                        Username = user.UserName,
                        Balance = balance, 
                        Portfolio = userPortfolio
                    });
            }

                // Kullanıcı oluşturulamadı, hataları döndürün.
                return BadRequest(result.Errors);
            }

            // Model geçersiz, hataları döndürün.
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var token = TokenHandler.CreateToken(_configuration, user);
                        var balance = _balanceService.GetUserBalance(user.Id); 
                        var userPortfolio = _portfolioService.GetPortfolioByUserId(user.Id);
                        
                        return Ok(new
                        {
                            Message = "Giriş başarılı.",
                            Token = token.AccessToken,
                            Username = user.UserName,
                            Balance = balance, // Kullanıcının bakiyesini buradan alabilirsiniz
                            Portfolio = userPortfolio // Kullanıcının sahip olduğu hisseleri buradan alabilirsiniz
                        });
                    }
                }

                // Giriş başarısız, hata döndürün.
                return BadRequest("Giriş başarısız.");
            }

            // Model geçersiz, hataları döndürün.
            return BadRequest(ModelState);
        }
        
        [Authorize("AdminOnly")]
        [HttpPost("updatesystembalance")]
        public IActionResult UpdateSystemBalance([FromBody] decimal newBalance)
        {
            _systemBalanceService.UpdateSystemBalance(newBalance);
            return Ok("Sistem bakiyesi güncellendi.");
        }

        [HttpGet("systembalance")]
        public IActionResult GetSystemBalance()
        {
            var systemBalance = _systemBalanceService.GetSystemBalance();
            return Ok(new { SystemBalance = systemBalance });
        }

    }


    // Bu modeli RegisterModel olarak varsayalım, yeni kullanıcı oluşturmak için gerekli bilgileri içerir.
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
    }

    // Bu modeli LoginModel olarak varsayalım, kullanıcı girişi için gerekli bilgileri içerir.
    public class LoginModel
    {
        public string Username{ get; set; }
        public string Password { get; set; }

    }
}