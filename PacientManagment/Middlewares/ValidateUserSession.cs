using Microsoft.AspNetCore.Http;
using PacientManagment.Core.Application.ViewModels.User;
using PacientManagment.Core.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using PacientManagment.Infrastructure.Persistence.Contexts;

namespace PacientManagment.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationContext _context;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor, ApplicationContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<bool> HasUser()
        {
            //UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            //if (userViewModel == null)
            //{
            //    return false;
            //}
            //return true;




            // Primero revisa si la sesión existe en HttpContext
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            if (userViewModel != null)
            {
                return true;
            }

            // Si no hay sesión en HttpContext, verifica la tabla Sessions en la base de datos
            var activeSession = await _context.Sessions
                .Where(s => s.UserName == _httpContextAccessor.HttpContext.User.Identity.Name) // O usa algún identificador persistente
                .FirstOrDefaultAsync();

            if (activeSession != null)
            {
                // Si existe una sesión activa en la base de datos, puedes restaurar los datos en HttpContext
                var user = await _context.Users
                    .Where(u => u.Username == activeSession.UserName)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    // Restaura la sesión en HttpContext
                    _httpContextAccessor.HttpContext.Session.Set("user", new UserViewModel
                    {
                        Username = user.Username,
                        Role = user.Role,
                        // Otros datos del usuario
                    });

                    return true;
                }
            }

            return false;


        }
    }
}
