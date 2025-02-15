using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacientManagment.Core.Domain.Common
{
    public class BaseBasicEntityWithCommon : BaseBasicEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
