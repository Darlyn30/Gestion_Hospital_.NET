using Microsoft.EntityFrameworkCore;
using PacientManagment.Infrastructure.Persistence.Contexts;

namespace PacientManagment.Middlewares
{
    public class RemoveSession
    {
        private readonly ApplicationContext _context;

        public RemoveSession(ApplicationContext context)
        {
            _context = context;
        }

        public async Task RemoveSessionAsync(string userName)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.UserName == userName);

            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }
    }
}
