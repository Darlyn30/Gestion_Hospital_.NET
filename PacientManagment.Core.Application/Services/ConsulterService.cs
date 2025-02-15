using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PacientManagment.Core.Application.Helpers;
using PacientManagment.Core.Application.Interfaces.Repositories;
using PacientManagment.Core.Application.Interfaces.Services;
using PacientManagment.Core.Application.ViewModels.Consulter;
using PacientManagment.Core.Application.ViewModels.User;
using PacientManagment.Core.Domain.Entities;

namespace PacientManagment.Core.Application.Services
{
    public class ConsulterService : IConsulterService
    {
        private readonly IConsulterRepository _consulterRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;
        public ConsulterService(IConsulterRepository consulterRepository, IHttpContextAccessor httpContextAccessor)
        {
            _consulterRepository = consulterRepository;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public async Task<SaveConsulterViewModel> Add(SaveConsulterViewModel vm)
        {
            Consulter consultory = new();
            consultory.Id = vm.Id;
            consultory.Name = vm.Name;
            consultory.Description = vm.Description;

            consultory = await _consulterRepository.AddAsync(consultory);

            SaveConsulterViewModel consulterVm = new();

            consulterVm.Id = vm.Id;
            consulterVm.Name = vm.Name;
            consulterVm.Description = vm.Description;
            
            return consulterVm;
        }

        public async Task Delete(int id)
        {
            var consultory = await _consulterRepository.GetByIdAsync(id);
            await _consulterRepository.DeleteAsync(consultory);
        }

        public async Task<List<ConsulterViewModel>> GetAllViewModel()
        {

            var consulterList = await _consulterRepository.GetAllWithIncludeAsync(new List<string> { "Medicers" });

            return consulterList.Select(consulter => new ConsulterViewModel
            {
                Name = consulter.Name,
                Description = consulter.Description,
                Id = consulter.Id,
                Quantity = consulter.Medicers.Where(medicer => medicer.UserId == userViewModel.Id).Count()
            }).ToList();

            
        }

        public async Task<SaveConsulterViewModel> GetByIdSaveViewModel(int id)
        {
            var consulter = await _consulterRepository.GetByIdAsync(id);

            SaveConsulterViewModel vm = new();
            vm.Id = consulter.Id;
            vm.Name = consulter.Name;
            vm.Description = consulter.Description;

            return vm;
        }

        public async Task Update(SaveConsulterViewModel vm)
        {
            Consulter consultory = await _consulterRepository.GetByIdAsync(vm.Id);
            consultory.Id = vm.Id;
            consultory.Name = vm.Name;
            consultory.Description = vm.Description;

            await _consulterRepository.UpdateAsync(consultory);
        }
    }
}
