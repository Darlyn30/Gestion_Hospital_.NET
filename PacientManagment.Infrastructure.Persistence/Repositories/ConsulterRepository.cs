using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacientManagment.Core.Application.Interfaces.Repositories;
using PacientManagment.Core.Domain.Entities;
using PacientManagment.Infrastructure.Persistence.Contexts;

namespace PacientManagment.Infrastructure.Persistence.Repositories
{
    public class ConsulterRepository : GenericRepository<Consulter>, IConsulterRepository
    {
        private readonly ApplicationContext _context;

        public ConsulterRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
