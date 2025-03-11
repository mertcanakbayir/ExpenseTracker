using System.Linq.Expressions;
using Business.Abstract;
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
        public void Add(User entity)
        {
            if (entity == null) {
                throw new ArgumentNullException("Kullanıcı boş olamaz");
            }
            _userRepository.Add(entity);
        }

        public void Delete(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Kullanıcı boş olamaz");
            }
            _userRepository.Delete(entity);
        }

        public bool Exists(Expression<Func<User, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            if (_userRepository.Exists(filter))
            {
                return _userRepository.Get(filter);
            }
            throw new InvalidOperationException("Kullanıcı bulunamadı!");
        }

        public List<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            return _userRepository.GetAll(filter);
        }

        public void Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Kullanıcı boş olamaz");
            }
            _userRepository.Update(entity);
        }
    }
}
