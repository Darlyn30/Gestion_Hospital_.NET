using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacientManagment.Core.Application.ViewModels.Consulter;

namespace PacientManagment.Core.Application.Interfaces.Services
{
    public interface IConsulterService : IGenericService<SaveConsulterViewModel, ConsulterViewModel>
    {
    }
}
