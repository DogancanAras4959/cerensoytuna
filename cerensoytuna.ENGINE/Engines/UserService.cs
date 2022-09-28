
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.COMMON.AccountDTO;
using cerensoytuna.COMMON.Helpers.Cyrptography;
using cerensoytuna.CORE.UnitOfWork;
using cerensoytuna.DAL;
using cerensoytuna.DAL.Models;
using cerensoytuna.ENGINES.Interface;

namespace cerensoytuna.ENGINES.Engines
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<cerensoytunadbcontext> _unitOfWork;
        //private readonly IUserRepository _userRepository;
        public UserService(/*IUserRepository userRepository*/ IUnitOfWork<cerensoytunadbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_userRepository = userRepository;
        }

        public bool DeleteUserById(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<Users>().DeleteAsync(new Users { Id = id });
            return Convert.ToBoolean(result.Result);
        }

        public UserDto GetUserById(int id)
        {
            Users user = _unitOfWork.GetRepository<Users>().FindAsync(x => x.Id == id).Result;
            if (user == null)
            {
                return new UserDto();
            }

            return new UserDto
            {
                UserName = user.UserName,
                IsActive = user.IsActive,
                CreatedTime = user.CreatedTime,
                UpdatedTime = user.UpdatedTime,
                DisplayName = user.DisplayName,
                ProfileImage = user.ProfileImage,
                Password = user.Password,
                Id = user.Id
            };
        }

        public UserDto GetUserByName(string name)
        {
            Users userGet = _unitOfWork.GetRepository<Users>().FindAsync(x => x.UserName == name).Result;
            string passwordDeCrypto = new Cyrpto().TryDecrypt(userGet.Password);

            if (userGet == null)
            {
                return new UserDto();
            }

            return new UserDto
            {
                UserName = userGet.UserName,
                DisplayName = userGet.DisplayName,
                ProfileImage = userGet.ProfileImage,
                Id = userGet.Id,
                CreatedTime = userGet.CreatedTime,
                UpdatedTime = userGet.UpdatedTime,
                IsActive = userGet.IsActive,
                Password = passwordDeCrypto,

            };
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            Users user = await _unitOfWork.GetRepository<Users>().FindAsync(x => x.UserName == email && x.Password == password);

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
            //return await _userRepository.LoginAsync(email, password);
        }

        public async Task<bool> Register(UserDto model)
        {
            model.Password = new Cyrpto().TryEncrypt(model.Password);

            Users newUser = await _unitOfWork.GetRepository<Users>().AddAsync(new Users
            {

                IsActive = model.IsActive,
                Password = model.Password,
                DisplayName = model.DisplayName,
                ProfileImage = model.ProfileImage,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                UserName = model.UserName
            });

            return newUser != null && newUser.Id != 0;
        }

        public async Task<bool> UpdateUser(UserDto userDto)
        {
            Users userGet = await _unitOfWork.GetRepository<Users>().FindAsync(x => x.Id == userDto.Id);
            string passwordCrypto = "";

            if (userDto.Password != null)
            {
                passwordCrypto = new Cyrpto().TryEncrypt(userDto.Password);
            }
            else
            {
                passwordCrypto = userGet.Password;
            }

            Users getUser = await _unitOfWork.GetRepository<Users>().UpdateAsync(new Users
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
                DisplayName = userDto.DisplayName,
                ProfileImage = userDto.ProfileImage,
                Password = passwordCrypto,
                UpdatedTime = DateTime.Now,
                CreatedTime = userDto.CreatedTime,
                IsActive = userGet.IsActive,

            });

            return getUser != null;
        }

        public List<UserListItemDto> UserList()
        {
            IEnumerable<Users> roles = _unitOfWork.GetRepository<Users>().Where(null, x => x.OrderBy(y => y.Id), "", 1, 30);

            return roles.Select(x => new UserListItemDto
            {
                UserName = x.UserName,
                UpdatedTime = x.UpdatedTime,
                CreatedTime = x.CreatedTime,
                Id = x.Id,
                DisplayName = x.DisplayName,
                ProfileImage = x.ProfileImage,
                Password = x.Password,
                IsActive = x.IsActive

            }).ToList();
        }
    }
}
