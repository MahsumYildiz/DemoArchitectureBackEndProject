using Core.DataAccess.EntityFramework;
using DataAccess.Context;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.OperationClaimRepository
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, SimpleContextDb>, IOperationClaimDal
    {

    }
}
