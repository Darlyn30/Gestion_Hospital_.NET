using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PacientManagment.Core.Application.ViewModels.Consulter;

namespace PacientManagment.Core.Application.ViewModels.Medicer
{
    public class SaveMedicerViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter a Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter a last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter a Phone number")]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        public string Phone { get; set; }



        [Required(ErrorMessage = "You must enter your ID credential")]
        [DataType(DataType.Text)]
        [MaxLength(11)]
        public string Cedula { get; set; } // ID, but would be duplicated prop

        public string? ImgPath { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "You must enter the Medic Consultory.")]
        public int ConsulterId { get; set; }
        public List<ConsulterViewModel>? Consulters { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public int UserId {  get; set; }
    }
}
