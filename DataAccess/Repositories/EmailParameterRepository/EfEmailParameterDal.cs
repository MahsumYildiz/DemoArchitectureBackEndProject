using Core.DataAccess.EntityFramework;
using DataAccess.Context;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EmailParameterRepository
{
    public class EfEmailParameterDal:EfEntityRepositoryBase<EmailParameter,SimpleContextDb>,IEmailParameterDal
    {

    }
}
