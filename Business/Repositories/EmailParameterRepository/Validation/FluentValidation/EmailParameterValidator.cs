using Entities.Concreate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.EmailParameterRepository.Validation.FluentValidation
{
    public class EmailParameterValidator:AbstractValidator<EmailParameter>
    {
        public EmailParameterValidator()
        {
            RuleFor(p => p.Smtp).NotEmpty().WithMessage("Smtp adresi boş olmaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail adresi boş olmaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre adresi boş olmaz");
            RuleFor(p => p.Port).NotEmpty().WithMessage("Port adresi boş olmaz");
           
        }
    }
}
