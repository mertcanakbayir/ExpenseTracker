using System.Linq.Expressions;
using Business.Abstract;
using Core.DTOs;
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
            if (userDto == null)
            {
                throw new ArgumentNullException("Kullanıcı bilgileri boş olamaz");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = System.Text.Encoding.UTF8.GetBytes("123456"), // Sabit şifre değeri
                PasswordSalt = new byte[0], // Geçici olarak boş salt
                RoleId = userDto.RoleId,
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            _userRepository.Add(user);
        }

        public void Delete(Guid id)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Kullanıcı boş olamaz");
            }
            _userRepository.Delete(entity);
        }

        public bool Exists(Expression<Func<User, bool>> filter)
        {
            return _userRepository.Exists(filter);
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            var user = _userRepository.Get(filter);
            if (user == null)
            {
                throw new InvalidOperationException("Kullanıcı bulunamadı!");
            }
            return user;
        }

        public List<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            return _userRepository.GetAll(filter);
        }

        public void Update(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException("Kullanıcı bilgileri boş olamaz");
            }

            var existingUser = _userRepository.Get(u => u.Email == userDto.Email);
            if (existingUser == null)
            {
                throw new InvalidOperationException("Güncellenecek kullanıcı bulunamadı");
            }

            existingUser.Username = userDto.Username;
            existingUser.RoleId = userDto.RoleId;
            existingUser.UpdateDate = DateTime.UtcNow;

            _userRepository.Update(existingUser);
        }
    }
}
