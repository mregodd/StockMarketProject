using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataTransferObject.Dtos.AppUserDtos
{
    public class AppUserRegisterDto //kullanıcı kayıt dtomuz bunu register validatore bağlıyoruz
    {
        [Required(ErrorMessage = "Lütfen isim giriniz.")]
        [StringLength(15, ErrorMessage = "Lütfen isminizi 3 ile 20 karakter arasında giriniz...", MinimumLength = 3)]
        [Display(Name = "İsim")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Lütfen soyisim giriniz.")]
        [StringLength(15, ErrorMessage = "Lütfen soyisminizi 2 ile 20 karakter arasında giriniz...", MinimumLength = 2)]
        [Display(Name = "Soyisim")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Lütfen emaili boş geçmeyiniz...")]
        [EmailAddress(ErrorMessage = "Lütfen email formatında mailinizi yazınız.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz.")]
        [DataType(DataType.Password, ErrorMessage = "Lütfen şifreyi tüm kuralları göz önüne alarak giriniz...")]
        [Display(Name = "Şifre")]
        public string Password  { get; set; }
        
        [Required(ErrorMessage = "Lütfen şifrenizi tekrar giriniz.")]
        [DataType(DataType.Password, ErrorMessage = "Lütfen aynı şifreyi giriniz.")]
        [Display(Name = "Şifre Tekrar")]
        public string ConfirmPassword { get; set; }

        
    }
}
