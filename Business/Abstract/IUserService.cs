using Core.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(UserDto userDto);

        void Update(Guid id,UserDto userDto);

        void Delete(Guid id);

        List<UserDto> GetAll();

        UserDto Get(Guid id);
    }
}
