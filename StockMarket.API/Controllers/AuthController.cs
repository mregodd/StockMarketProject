using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Entities.Concrete;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserNumber = model.UserNumber,
                    // Diğer kullanıcı bilgilerini buraya ekleyin, gerekirse kullanıcı oluşturulmadan önce doğrulama yapın.
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Kullanıcı başarıyla oluşturuldu, isterseniz burada diğer işlemleri gerçekleştirebilirsiniz.
                    return Ok("Kullanıcı başarıyla oluşturuldu.");
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
                var result = await _signInManager.PasswordSignInAsync(model.UserNumber, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Giriş başarılı, isterseniz burada diğer işlemleri gerçekleştirebilirsiniz.
                    return Ok("Giriş başarılı.");
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
        public string UserNumber { get; set; }
        public decimal UserBalance { get; set; }
        public string Password { get; set; }
    }

    // Bu modeli LoginModel olarak varsayalım, kullanıcı girişi için gerekli bilgileri içerir.
    public class LoginModel
    {
        public string UserNumber { get; set; }
        public string Password { get; set; }

    }
}