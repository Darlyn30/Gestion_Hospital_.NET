using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PacientManagment.Core.Application.Interfaces.Services;
using PacientManagment.Core.Application.Services;

namespace PacientManagment.Core.Application
{
    public static class ServiceRegister
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMedicerService, MedicerService>();
            services.AddTransient<IConsulterService,  ConsulterService>();
            #endregion
        }
    }
}
