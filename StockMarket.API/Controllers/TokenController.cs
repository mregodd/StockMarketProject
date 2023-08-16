using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockMarket.API.Security;
using StockMarket.Entities.Concrete;
using System.Threading.Tasks;

namespace StockMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public TokenController(IConfiguration configuration, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    // Giriş başarılı, token oluştur ve dön
                    var token = TokenHandler.CreateToken(_configuration, user);
                    return Ok(new { Token = token.AccessToken });
                }
            }

            // Giriş başarısız, hata döndür
            return BadRequest("Giriş başarısız.");
        }
    }
}

