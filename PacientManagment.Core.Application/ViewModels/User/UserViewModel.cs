using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacientManagment.Core.Application.ViewModels.Medicer;
using PacientManagment.Core.Domain.Entities;

namespace PacientManagment.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public string Role {  get; set; }
        public ICollection<MedicerViewModel> Medicers { get; set; }
    }
}
