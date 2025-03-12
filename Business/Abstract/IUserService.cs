using Core.Business;
using Core.DTOs;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService:IBaseService<UserDto,User>
    {
    }
}
