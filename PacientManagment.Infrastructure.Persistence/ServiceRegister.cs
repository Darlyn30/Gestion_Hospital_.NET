using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PacientManagment.Core.Application.Interfaces.Repositories;
using PacientManagment.Infrastructure.Persistence.Contexts;
using PacientManagment.Infrastructure.Persistence.Repositories;

namespace PacientManagment.Infrastructure.Persistence
{
    public static class ServiceRegister
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AppConnection"),
            m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMedicerRepository, MedicerRepository>();
            services.AddTransient<IConsulterRepository, ConsulterRepository>();
            #endregion
        }
    }
}
