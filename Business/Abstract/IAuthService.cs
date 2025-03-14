using Core.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        string Register(RegisterDto registerDto);

        string Login(LoginDto loginDto);


    }
}
