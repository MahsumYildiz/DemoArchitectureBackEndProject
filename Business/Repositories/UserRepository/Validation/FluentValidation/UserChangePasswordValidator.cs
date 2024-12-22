using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserChangePasswordValidator:AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Şifre boş olamaz");
            RuleFor(p => p.NewPassword).MinimumLength(6).WithMessage("Şifre en az 6 karekter olmalı");
        }
    }
}
