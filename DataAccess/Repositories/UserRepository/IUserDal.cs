using Core.DataAccess;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepository
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetUserOperationClaims(int userId);


    }
}
