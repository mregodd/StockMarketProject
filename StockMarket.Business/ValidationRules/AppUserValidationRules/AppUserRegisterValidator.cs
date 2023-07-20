using FluentValidation;
using StockMarket.DataTransferObject.Dtos.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.ValidationRules.AppUserValidationRules
{
    public class AppUserRegisterValidator : AbstractValidator<AppUserRegisterDto> //appuser register dtomuzu buraya bağladık ve validationlarımızı girdik
    {
       public AppUserRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim kısmı boş bırakılamaz.");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyisim kısmı boş bırakılamaz.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email kısmı boş bırakılamaz.");
            
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre kısmı boş bırakılamaz.");
            
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre doğrulama kısmı boş bırakılamaz.");

            RuleFor(x => x.Name).MaximumLength(35).WithMessage("Lütfen en fazla 35 karakter kullanınız.");

            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Lütfen en az 2 karakter kullanınız.");

            RuleFor(x => x.ConfirmPassword).Equal(y=> y.Password).WithMessage("Parolalar eşleşmiyor.");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir mail adresi giriniz");
        }
    }
}
