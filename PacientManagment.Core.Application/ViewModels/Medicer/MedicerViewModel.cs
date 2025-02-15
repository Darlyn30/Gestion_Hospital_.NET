using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacientManagment.Core.Application.ViewModels.Medicer
{
    public class MedicerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cedula { get; set; } // ID, but would be duplicated prop
        public string? ImgPath { get; set; }
        public int ConsulterId { get; set; }
        public string ConsulterName { get; set; }
    }
}
