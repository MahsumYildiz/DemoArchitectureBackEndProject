﻿using Core.Utilities.Result.Abstract;
using Entities.Concreate;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository
{
    public interface IUserService
    {
        void Add(RegisterAuthDto authDto);
        IResult Update(User user);
        IResult ChangePassword(UserChangePasswordDto userChangePasswordDto);
        IResult Delete(User user);
        IDataResult< List<User>> GetList();
        User GetByEmail(string email);
        List<OperationClaim> GetUserOperationClaims(int userId);
        IDataResult<User> GetById(int id);
    }
}
