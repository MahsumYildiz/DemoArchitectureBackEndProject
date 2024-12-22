using Entities.Concreate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserOperationClaimRepository.FluentValidation
{
    public class UserOperationClaimValidator:AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
                RuleFor(p=>p.UserId).NotEmpty().WithMessage("Yetki ataması için kullanıcı seç");
                RuleFor(p=>p.OperationClaimId).NotEmpty().WithMessage("Yetki ataması için yetki seçimi seç");

        }
    }
}
