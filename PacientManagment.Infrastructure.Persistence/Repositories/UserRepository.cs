using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PacientManagment.Core.Application.Helpers;
using PacientManagment.Core.Application.Interfaces.Repositories;
using PacientManagment.Core.Application.Interfaces.Services;
using PacientManagment.Core.Application.ViewModels.User;
using PacientManagment.Core.Domain.Entities;
using PacientManagment.Infrastructure.Persistence.Contexts;

namespace PacientManagment.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryptation.ComputeSha256Hash(entity.Password);
            await base.AddAsync(entity);
            return entity;
        }

        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordEncryptation.ComputeSha256Hash(loginVm.Password);
            User user = await _context.Set<User>().FirstOrDefaultAsync(user => user.Username == loginVm.Username && user.Password == passwordEncrypt);
            return user;
        }
    }
}
