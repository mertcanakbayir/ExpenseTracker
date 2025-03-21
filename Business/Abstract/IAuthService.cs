using Core.DTOs;
using Core.Security.JWT;

namespace Business.Abstract
{
    public interface IAuthService
    {
        List<string> Register(RegisterDto registerDto);

        AccessToken Login(LoginDto loginDto);


    }
}
