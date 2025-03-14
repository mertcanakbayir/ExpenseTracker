using Business.Abstract;
using Core.DTOs;
using Core.Utilities;
using DAL.Abstract;
using Entities.Concrete;


namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string Login(LoginDto loginDto)
        {
            var user=_userRepository.Get(u=>u.Email==loginDto.Email);
            if (user == null)
            {
                return "Kullanıcı sistemde kayıtlı değil.";
            }

            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (!result) {
                return "Kullanıcı adı veya şifre yanlış.";
            }

            return "Giriş yapıldı.";

        }

        public string Register(RegisterDto registerDto)
        {
            if (_userRepository.Exists(u => u.Email == registerDto.Email))
            {
                return "Bu mail adresi kullanımda.";
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
            return "Kullanıcı Kaydı Başarılı.";
        }
    }
}
