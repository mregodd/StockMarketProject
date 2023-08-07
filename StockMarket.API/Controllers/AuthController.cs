using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockMarket.API.Security;
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
        private readonly PortfolioManager _portfolioManager;
        private readonly IConfiguration _configuration;


        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, PortfolioManager portfolioManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _portfolioManager = portfolioManager;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Username,
                    Name = "User",
                    Surname = "USER",
                    City = "UserCity",
                    District = "UserDistrict",

                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Kullanıcı rolü eklemesi yapıldı
                    if (!await _userManager.IsInRoleAsync(user, "USERROLE"))
                    {
                        await _userManager.AddToRoleAsync(user, "USERROLE");
                    }

                    var portfolio = new UserPortfolio
                    {
                        StockName = "Example Stock",
                        Quantity = 100,
                        Value = 1000,
                        AppUser = user // Kullanıcı kimliğini atayın
                    };

                    _portfolioManager.AddPortfolio(portfolio);

                    // Token oluşturup döndürme
                    var token = TokenHandler.CreateToken(_configuration, user);

                    // Token'i kullanarak bir işlem gerçekleştirebilirsiniz
                    return Ok(new { Token = token.AccessToken });
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
                        return Ok(new { Token = token.AccessToken });
                    }
                }

                // Giriş başarısız, hata döndürün.
                return BadRequest("Giriş başarısız.");
            }

            // Model geçersiz, hataları döndürün.
            return BadRequest(ModelState);
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