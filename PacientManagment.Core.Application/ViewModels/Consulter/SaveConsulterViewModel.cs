using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacientManagment.Core.Application.ViewModels.Consulter
{
    public class SaveConsulterViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre de la categoria")]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
