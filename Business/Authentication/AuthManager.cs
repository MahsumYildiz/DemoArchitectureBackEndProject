using Business.Authentication.Validation.FluentValidation;
using Business.Repositories.UserRepository;
using Core.Aspect.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Concreate;
using Entities.Dtos;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;


        public AuthManager(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public IDataResult<Token> Login(LoginAuthDto loginDto)
        {
            var user = _userService.GetByEmail(loginDto.Email);
            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            List<OperationClaim> operationClaims = _userService.GetUserOperationClaims(user.Id); 
            if(result)
            {
                Token token = new Token();
                token = _tokenHandler.CreateToken(user, operationClaims);
                return new SuccesDataResult<Token>(token);
            }
            return new ErrorDataResult<Token>("Kullanıcı maili ya da şifre bilgisi yanlış");
        }
        [ValidationAspect(typeof(AuthValidator))]
        public IResult Register(RegisterAuthDto registerDto)
        {
            int imgSize = 1;
            IResult result = BusinessRules.Run(
                CheckIfEmailExists(registerDto.Email),
                CheckIfImageExtensionsAllow(registerDto.Image.FileName),
                CheckIfImageSizeIsLessThanOneMb(registerDto.Image.Length));


            if (result != null)
            {
                return result;
            }

            _userService.Add(registerDto);
            return new SuccessResult("Kullanıcı kaydı başarılı");

        }
        private IResult CheckIfEmailExists(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
            {
                return new ErrorResult("Bu mail adresi daha önce kullanılmış");
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageSizeIsLessThanOneMb(long imgSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imgSize * 0.000001);
            if (imgMbSize > 1)
            {
                return new ErrorResult("Yüklediğiniz resim boyutu max 1 mb olmalı");
            }
            return new SuccessResult();
        }
        private IResult CheckIfImageExtensionsAllow(string fileName)
        {

            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extensions = ext.ToLower();
            List<string> AllowFileExtensions = new List<string>() { ".jpg", ".jpeg", ".gif", ".png" };
            if (!AllowFileExtensions.Contains(extensions))
            {
                return new ErrorResult("Eklediğiniz resim jpg ,jpeg ,gif ,png türünden olmalıdır");
            }
            return new SuccessResult();
        }
    }
}
