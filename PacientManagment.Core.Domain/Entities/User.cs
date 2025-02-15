using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacientManagment.Core.Domain.Common;

namespace PacientManagment.Core.Domain.Entities
{
    public class User : BaseBasicEntityWithCommon
    {
        public string LastName { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }
        public string Role {  get; set; }

        //nav prop
        public ICollection<Medicer>? Medicers { get; set; }
    }
}
