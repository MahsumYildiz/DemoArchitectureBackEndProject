using Business.Aspect.Secured;
using Business.Repositories.OperationClaimRepository.Constans;
using Business.Repositories.OperationClaimRepository.Validation.FluentValidation;
using Core.Aspect.Performance;
using Core.Aspect.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.OperationClaimRepository;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.OperationClaimRepository
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }
        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            IResult result=BusinessRules.Run(IsNameExistForAdd(operationClaim.Name));
            {
                if(result !=null)
                {
                    return result;
                }
            }
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(OperationClaimMessages.Added);
        }
        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(IsNameExistForUpdate(operationClaim));
            {
                if (result != null)
                {
                    return result;
                }
            }
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(OperationClaimMessages.Update);
        }
        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(OperationClaimMessages.Deleted);
        }
        [SecuredAspect()]
        [PerformanceAspect()]
        public IDataResult<List<OperationClaim>> GetList()
        {
            return new SuccesDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        IDataResult<OperationClaim> IOperationClaimService.GetById(int id)
        {
            return new SuccesDataResult<OperationClaim>(_operationClaimDal.Get(p => p.Id == id));
        }
        private IResult IsNameExistForAdd(string name)
        {
            var result=_operationClaimDal.Get(p=>p.Name == name);
            if (result != null) 
            {
                return new ErrorResult(OperationClaimMessages.NameIsNotAvailable);
            }
            return new SuccessResult();
        }
        private IResult IsNameExistForUpdate(OperationClaim operationClaim)
        {
            var currentOperationClaim= _operationClaimDal.Get(p => p.Id == operationClaim.Id);
            if(currentOperationClaim.Name!=operationClaim.Name) 
            {
                var result = _operationClaimDal.Get(p => p.Name == operationClaim.Name);
                if (result != null)
                {
                    return new ErrorResult(OperationClaimMessages.NameIsNotAvailable);
                }
               
            }
            return new SuccessResult();
        }
    }
}
