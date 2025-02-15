using Microsoft.EntityFrameworkCore;
using PacientManagment.Core.Application.ViewModels.User;
using PacientManagment.Core.Domain.Entities;
using PacientManagment.Infrastructure.Persistence.Contexts;

namespace PacientManagment.Middlewares
{
    public class SessionConsult
    {
        private readonly ApplicationContext _context;

        public SessionConsult(ApplicationContext context)
        {
            _context = context;
        }

        public async Task InsertSessionAsync(UserViewModel userVm)
        {
            var session = new Session
            {
                UserName = userVm.Username,
                Role = userVm.Role,
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> isAdmin()
        {

            var session = await _context.Sessions
                .FirstOrDefaultAsync();

            if (session != null)
            {
                Console.WriteLine($"ROL : {session.Role}");
                // Verifica si el rol es admin
                if (session.Role == "Administrator")
                {
                    return true;
                }
                return false;
            }

            return false; // Si no se encuentra la sesión para el usuario
        }

        public async Task<bool> isData()
        {
            var data = await _context.Sessions
                .FirstOrDefaultAsync();

            if (data != null)
            {
                return true;
            }
            return false;
        }

        public async Task deleteAll()
        {
            // Obtener todos los usuarios de la tabla
            var allUsers = _context.Sessions.ToList(); // Obtener todos los registros

            // Eliminar todos los usuarios
            _context.Sessions.RemoveRange(allUsers);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }
    }
}
