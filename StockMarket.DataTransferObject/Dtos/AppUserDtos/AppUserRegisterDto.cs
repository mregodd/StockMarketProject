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
        //[Required(ErrorMessage ="İsim kısmı boş bırakılamaz.")]
        //[Display(Name ="İsim:")]
        //[MaxLength(35,ErrorMessage ="En fazla 35 karakter girebilirsiniz.")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password  { get; set; }
        public string ConfirmPassword { get; set; }

        
    }
}
