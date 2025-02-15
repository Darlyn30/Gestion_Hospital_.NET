using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacientManagment.Core.Domain.Common;

namespace PacientManagment.Core.Domain.Entities
{
    public class Consulter : BaseBasicEntityWithCommon
    {
        public string Description { get; set; }
        public ICollection<Medicer>? Medicers { get; set; }
    }
}
