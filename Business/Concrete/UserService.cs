using Business.Abstract;
using Core.DTOs;
using Core.Utilities;
using DAL.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Add(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Password))
                throw new ArgumentException("Şifre boş olamaz!");

            HashingHelper.CreatePassword(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = userDto.RoleId
            };

            _userRepository.Add(user);
        }

        public void Update(Guid id, UserDto userDto)
        {
            var existingUser = _userRepository.Get(u=>u.Id==id);
            if (existingUser == null)
                throw new Exception("Kullanıcı bulunamadı!");

            existingUser.Username = userDto.Username;
            existingUser.Email = userDto.Email;
            existingUser.RoleId = userDto.RoleId;

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                HashingHelper.CreatePassword(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                existingUser.PasswordHash = passwordHash;
                existingUser.PasswordSalt = passwordSalt;
            }

            _userRepository.Update(existingUser);
        }

        public void Delete(Guid id)
        {
            var user = _userRepository.Get(u => u.Id == id);
            if (user == null) throw new Exception("Kullanıcı Bulunamadı!");
        }

        public UserDto Get(Guid id)
        {
            var user = _userRepository.Get(u=>u.Id==id);
            if (user == null) {
                return null;
            }

            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                RoleId = user.RoleId,
            };
        }

        public List<UserDto> GetAll()
        {
            var users=_userRepository.GetAll();
            return users.Select(user=>new UserDto
            {
                Username=user.Username,
                Email=user.Email,
                RoleId=user.RoleId,

            }).ToList();
        }

        public void Update(UserDto userDto)
        {
            throw new NotImplementedException();
        }

    }
}
