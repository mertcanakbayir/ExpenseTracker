using Core.DTOs;
using FluentValidation;

namespace Core.Utilities.Validation
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-mail alanı boş olamaz.")
               .EmailAddress().WithMessage("Lütfen geçerli bir mail adresi girin.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz")
                .MinimumLength(5).WithMessage("Şifre minimum 5 karakter olmalıdır.");

            RuleFor(x => x.Username).NotEmpty().WithMessage("İsim alanı boş olamaz.").MinimumLength(5).WithMessage("Kullanıcı adı alanı minimum 5 karakter olmalı.");

            
        }
    }
}
