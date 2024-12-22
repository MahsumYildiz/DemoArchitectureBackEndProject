﻿using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHandler
    {
        Token CreateToken(User user,List<OperationClaim>operationClaims);
    }
}
