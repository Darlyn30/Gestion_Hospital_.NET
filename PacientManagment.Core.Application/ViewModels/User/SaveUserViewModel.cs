using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacientManagment.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter a password")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [Required(ErrorMessage = "You must enter a password")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "You must enter a name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must enter a last name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter an email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter a phone number")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
        [Required]
        public string Role {  get; set; }

    }
}
