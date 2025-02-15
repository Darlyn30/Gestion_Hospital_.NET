using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacientManagment.Core.Application.Helpers;
using PacientManagment.Core.Application.Interfaces.Repositories;
using PacientManagment.Core.Application.Interfaces.Services;
using PacientManagment.Core.Application.ViewModels.User;
using PacientManagment.Core.Domain.Entities;

namespace PacientManagment.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            UserViewModel userVm = new();
            User user = await _userRepository.LoginAsync(vm);

            if (user == null)
            {
                return null;
            }

            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.LastName = user.LastName;
            userVm.Username = user.Username;
            userVm.Phone = user.Phone;
            userVm.Password = user.Password;
            userVm.Email = user.Email;
            userVm.Role = user.Role;

            return userVm;
        }

        public async Task Update(SaveUserViewModel vm)
        {
            User user = await _userRepository.GetByIdAsync(vm.Id);
            user.Id = vm.Id;
            user.Name = vm.Name;
            user.LastName = vm.LastName;
            user.Username = vm.Username;
            user.Password = PasswordEncryptation.ComputeSha256Hash(vm.Password); ;
            user.Phone = vm.Phone;
            user.Email = vm.Email;
            user.Role = vm.Role;
            
            await _userRepository.UpdateAsync(user);
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            User user = new();
            user.Name = vm.Name;
            user.LastName = vm.LastName;
            user.Username = vm.Username;
            user.Password = vm.Password;
            user.Phone = vm.Phone;
            user.Email = vm.Email;
            user.Role = vm.Role;

            user = await _userRepository.AddAsync(user);

            SaveUserViewModel userVm = new();

            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.LastName = user.LastName;
            userVm.Phone = user.Phone;
            userVm.Email = user.Email;
            userVm.Username = user.Username;
            userVm.Password = user.Password;
            userVm.Role = user.Role;

            return userVm;
        }

        public async Task Delete(int id)
        {
            var product = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(product);
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            SaveUserViewModel vm = new();
            vm.Id = user.Id;
            vm.Name = user.Name;
            vm.LastName = user.LastName;
            vm.Username = user.Username;
            vm.Password = user.Password;
            vm.Phone = user.Phone;
            vm.Email = user.Email;
            vm.Role = user.Role;
            return vm;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _userRepository.GetAllWithIncludeAsync(new List<string> { "Medicers" });

            return userList.Select(user => new UserViewModel
            {
                Name = user.Name,
                LastName = user.LastName,
                Username = user.Username,
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                Role = user.Role,
            }).ToList();
        }

        public async Task<List<UserViewModel>> GetByNameAsync(string userName)
        {
            var userList = await _userRepository.GetAllWithIncludeAsync(new List<string> { "Medicers" });
            var listViewModels = userList.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName= user.LastName,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                Username = user.Username,
                Role = user.Role,

            }).ToList();

            if (userName != null)
            {
                listViewModels = listViewModels.Where(user => user.Username.ToLower().Contains(userName.ToLower())).ToList();
            }

            return listViewModels;
        }
    }
}
