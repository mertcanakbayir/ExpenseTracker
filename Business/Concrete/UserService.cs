using AutoMapper;
using Business.Abstract;
using Core.Business;
using Core.DTOs;
using Core.Utilities;
using DAL.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserService:BaseService<UserDto,User>,IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,IMapper mapper):base(userRepository,mapper) {
            
        _userRepository = userRepository;
            _mapper = mapper;
        }

        public override void Add(UserDto dto)
        {
            if (_userRepository.Exists(u => u.Email == dto.Email))
            {
                throw new InvalidOperationException("Bu email zaten kayıtlı.");
            }

            HashingHelper.CreatePassword(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var userEntity=_mapper.Map<User>(dto);
            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;
            base.AddEntity(userEntity);
        }

        public override void Update(UserDto dto)
        {
            var existingUser=_userRepository.Get(u=>u.Email == dto.Email);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("Güncellenecek kullanıcı bulunamadı.");
            }
            base.Update(dto);
        }

    }
}
