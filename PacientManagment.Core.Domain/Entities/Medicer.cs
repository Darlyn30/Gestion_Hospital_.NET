using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacientManagment.Core.Domain.Common;

namespace PacientManagment.Core.Domain.Entities
{
    public class Medicer : BaseBasicEntityWithCommon
    {
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cedula { get; set; } // ID, but would be duplicated prop
        public string? ImgPath { get; set; }

        public int ConsulterId { get; set; }
        //nav prop
        public Consulter? Consulter { get; set; }

        public int UserId {  get; set; }
        //nav prop
        public User? User {  get; set; } 
    }
}
