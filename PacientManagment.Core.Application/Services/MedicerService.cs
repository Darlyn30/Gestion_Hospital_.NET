using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PacientManagment.Core.Application.Helpers;
using PacientManagment.Core.Application.Interfaces.Repositories;
using PacientManagment.Core.Application.Interfaces.Services;
using PacientManagment.Core.Application.ViewModels.Medicer;
using PacientManagment.Core.Application.ViewModels.User;
using PacientManagment.Core.Domain.Entities;

namespace PacientManagment.Core.Application.Services
{
    public class MedicerService : IMedicerService
    {
        private readonly IMedicerRepository _medicerRepository;
        private readonly UserViewModel userViewModel;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public MedicerService(IMedicerRepository medicerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _medicerRepository = medicerRepository;
            _httpcontextAccessor = httpContextAccessor;
            userViewModel = _httpcontextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<SaveMedicerViewModel> Add(SaveMedicerViewModel vm)
        {
            Medicer medicer = new();
            medicer.Name = vm.Name;
            medicer.LastName = vm.LastName;
            medicer.ImgPath = vm.ImgPath;
            medicer.Phone = vm.Phone;
            medicer.Email = vm.Email;
            medicer.Cedula = vm.Cedula;
            medicer.ConsulterId = vm.ConsulterId;
            medicer.UserId = userViewModel.Id;

            medicer = await _medicerRepository.AddAsync(medicer);

            SaveMedicerViewModel medicerVm = new();
            
            medicerVm.Id = vm.Id;
            medicerVm.Name = medicer.Name;
            medicerVm.LastName = medicer.LastName;
            medicerVm.ImgPath = medicer.ImgPath;
            medicerVm.Phone = medicer.Phone;
            medicerVm.Email = medicer.Email;
            medicerVm.Cedula= medicer.Cedula;
            medicerVm.ConsulterId = medicer.ConsulterId;

            return medicerVm;

        }

        public async Task Delete(int id)
        {
            var medicer = await _medicerRepository.GetByIdAsync(id);
            await _medicerRepository.DeleteAsync(medicer);
        }

        public async Task<List<MedicerViewModel>> GetAllViewModel()
        {
            var medicerList = await _medicerRepository.GetAllWithIncludeAsync(new List<string> { "Consulter" });

            return medicerList.Where(medicer => medicer.UserId == userViewModel.Id).Select(medicer => new MedicerViewModel
            {
                Name = medicer.Name,
                LastName = medicer.LastName,
                Email = medicer.Email,
                ImgPath = medicer.ImgPath,
                Phone = medicer.Phone,
                Cedula = medicer.Cedula,
                ConsulterName = medicer.Consulter.Name,
                ConsulterId = medicer.Consulter.Id,
            }).ToList();
        }

        public async Task<SaveMedicerViewModel> GetByIdSaveViewModel(int id)
        {
            var medicer = await _medicerRepository.GetByIdAsync(id);

            SaveMedicerViewModel vm = new();
            vm.Id = medicer.Id;
            vm.Name = medicer.Name;
            vm.LastName = medicer.LastName;
            vm.ImgPath = medicer.ImgPath;
            vm.Phone = medicer.Phone;
            vm.Email = medicer.Email;
            vm.Cedula = medicer.Cedula;
            vm.ConsulterId = medicer.ConsulterId;

            return vm;
        }

        public async Task<List<MedicerViewModel>> GetByNameAsync(string name)
        {
            var medicerList = await _medicerRepository.GetAllWithIncludeAsync(new List<string> { "Consulter"});
            var listViewModels = medicerList.Select(medicer => new MedicerViewModel
            {
                Id = medicer.Id,
                Name = medicer.Name,
                LastName = medicer.LastName,
                Email = medicer.Email,
                Phone = medicer.Phone,
                Cedula = medicer.Cedula,
                ConsulterId = medicer.Consulter != null ? medicer.Consulter.Id : 0,
                ConsulterName = medicer.Consulter != null ? medicer.Consulter.Name : null,

            }).ToList();

            if (name != null)
            {
                listViewModels = listViewModels.Where(user => user.Name.ToLower().Contains(name.ToLower())).ToList();
            }

            return listViewModels;
        }

        public async Task Update(SaveMedicerViewModel vm)
        {
            Medicer medicer = await _medicerRepository.GetByIdAsync(vm.Id);

            medicer.Id = vm.Id;
            medicer.Name = vm.Name;
            medicer.LastName = vm.LastName;
            medicer.Email = vm.Email;
            medicer.Phone = vm.Phone;
            medicer.ImgPath = vm.ImgPath;
            medicer.Cedula = vm.Cedula;
            medicer.ConsulterId = vm.ConsulterId;
            
            await _medicerRepository.UpdateAsync(medicer);
        }
    }
}
