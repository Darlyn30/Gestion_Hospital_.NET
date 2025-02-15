using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacientManagment.Core.Application.ViewModels.Medicer;
using PacientManagment.Core.Application.ViewModels.User;

namespace PacientManagment.Core.Application.Interfaces.Services
{
    public interface IMedicerService : IGenericService<SaveMedicerViewModel, MedicerViewModel>
    {
        Task<List<MedicerViewModel>> GetByNameAsync(string name);
    }
}
