using Business.Repositories.UserRepository.Contans;
using Business.Repositories.UserRepository.Validation.FluentValidation;
using Business.Utilities.File;
using Core.Aspect.Caching;
using Core.Aspect.Transaction;
using Core.Aspect.Validation;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.UserRepository;
using Entities.Concreate;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IFileService _fileService;

        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        [RemoveCacheAspect("User.GetList")]
        public  void Add(RegisterAuthDto registerDto)
        {

            string fileName = _fileService.FileSaveToServer(registerDto.Image, "./Content/Img/");
            var user = CreateUser(registerDto, fileName);

            _userDal.Add(user);

        }
        private User CreateUser(RegisterAuthDto registerDto, string fileName)
        {
            byte[] passwordHask, PasswordSalt;
            HashingHelper.CreatePassword(registerDto.Password, out passwordHask, out PasswordSalt);

            User user = new User();
            user.Id = 0;
            user.Email = registerDto.Email;
            user.Name = registerDto.Name;
            user.PasswordHash = passwordHask;
            user.PasswordSalt = PasswordSalt;
            user.ImageUrl = " ";
            return user;
        }

        public User GetByEmail(string email)
        {
            var result = _userDal.Get(p => p.Email == email);
            return result;
        }

        [ValidationAspect(typeof(UserValidator))]
        [TransactionAspect()]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(UserMessages.UpdatedUser);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(UserMessages.DeletedUser);
        }

        public IDataResult<List<User>> GetList()
        {
            return new SuccesDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int id)
        {
            var user = _userDal.Get(p => p.Id == id); // id ile tek kullanıcı alınıyor
            return new SuccesDataResult<User>(user);
        }

        public IResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var user = _userDal.Get(p => p.Id == userChangePasswordDto.UserId);
            bool result=HashingHelper.VerifyPasswordHash(userChangePasswordDto.CurrentPassword,user.PasswordHash,user.PasswordSalt);
            if(result)
            {
                return new ErrorResult(UserMessages.WrongCurrentPassword);
            }
            
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userChangePasswordDto.NewPassword,out passwordHash,out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;   
            _userDal.Update(user);
            return new SuccessResult(UserMessages.PasswordChanged);
        }

        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            return _userDal.GetUserOperationClaims(userId);
        }
    }
}
