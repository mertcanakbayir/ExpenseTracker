using Core.DTOs;
using FluentValidation;

namespace Core.Utilities.Validation
{
    public class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().WithMessage("Email alanı boş olamaz.").EmailAddress().WithMessage("Lütfen geçerli bir mail adresi girin.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz.").MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı.");
        }
    }
}
