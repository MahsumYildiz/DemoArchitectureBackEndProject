using Core.DataAccess.EntityFramework;
using DataAccess.Context;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserOperationClaimRepository
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, SimpleContextDb>, IUserOperationClaimDal
    {

    }
}
