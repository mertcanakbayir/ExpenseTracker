using Business.Abstract;
using Core.DTOs;
using Core.Security.JWT;
using Core.Utilities;
using Core.Utilities.Validation;
using DAL.Abstract;
using Entities.Concrete;
using FluentValidation;


namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        public AuthService(IUserRepository userRepository,ITokenHelper tokenHelper,IValidator<LoginDto> loginValidator,IValidator<RegisterDto> registerValidator)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;

        }
        public AccessToken Login(LoginDto loginDto)
        {
            var validateResult=ValidationTool.Validate(_loginValidator, loginDto);
            if(validateResult.Count>0)
            {
                return null;
            }
            var user = _userRepository.Get(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return null;
            }

            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (!result)
            {
                return null;
            }

            var token = _tokenHelper.CreateToken(user.Id, user.Email, user.RoleId.ToString());

            return token;
        
        }


        public List<string> Register(RegisterDto registerDto)
        {
            var validationResult = ValidationTool.Validate(_registerValidator, registerDto);
            if (validationResult.Count > 0)
            {
                return validationResult;
            }

            if (_userRepository.Exists(u => u.Email == registerDto.Email))
            {
                return new List<string> { "Bu mail adresi kullanımda." };
            }

            HashingHelper.CreatePassword(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")
            };

            _userRepository.Add(user);
            return new List<string> { "Kullanıcı Kaydı Başarılı." };
        }

    }
}
