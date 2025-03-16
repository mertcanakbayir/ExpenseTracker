using Core.DTOs;
using Core.Security.JWT;

namespace Business.Abstract
{
    public interface IAuthService
    {
        string Register(RegisterDto registerDto);

        AccessToken Login(LoginDto loginDto);


    }
}
