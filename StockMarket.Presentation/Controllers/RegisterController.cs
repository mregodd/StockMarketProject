using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarket.DataTransferObject.Dtos.AppUserDtos;
using StockMarket.Entities.Concrete;

namespace StockMarket.Presentation.Controllers
{
    public class RegisterController : Controller //kayıt işlemleri için controllerimizi şekilldendirdik
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid) //register bilgilerini appuser verilerinden atama işlemi yaptık
            {
                AppUser appUser = new AppUser()
                {
                    Name = appUserRegisterDto.Name,
                    UserName = appUserRegisterDto.Name,
                    Surname = appUserRegisterDto.Surname,
                    Email = appUserRegisterDto.Email,
                    City = "Bursa",
                    District = "Nilüfer",

                };
                var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);
                if (result.Succeeded) //şifre kayıt kısmı
                { 
                return RedirectToAction("Index");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                
            }
            return View();
        }
    }
}
//En az 6 karakterden oluşmalı
//En az 1 Küçük harf
//En az 1 Büyük harf
//En az 1 sembom
//En az 1 sayı içermeliler.