using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PacientManagment.Core.Domain.Common;
using PacientManagment.Core.Domain.Entities;

namespace PacientManagment.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Consulter> Consulters { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Medicer> Medics { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseBasicEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region general props

            #region Tables
            modelBuilder.Entity<User>()
            .ToTable("Users");
            modelBuilder.Entity<Consulter>()
                .ToTable("Consulters");
            modelBuilder.Entity<Session>()
                .ToTable("Sessions");
            modelBuilder.Entity<Medicer>()
                .ToTable("Medics");
            #endregion

            #region PK
            modelBuilder.Entity<User>()
            .HasKey(user => user.Id);

            modelBuilder.Entity<Consulter>()
                .HasKey(consulter =>  consulter.Id);
            modelBuilder.Entity<Session>()
                .HasKey(session => session.Id);
            modelBuilder.Entity<Medicer>()
                .HasKey(Medicer => Medicer.Id);
            #endregion

            #region FK

            modelBuilder.Entity<Consulter>()
                .HasMany<Medicer>(consultory => consultory.Medicers)
                .WithOne(medicer => medicer.Consulter)
                .HasForeignKey(medicer => medicer.ConsulterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Medicer>(user =>  user.Medicers)
                .WithOne(medicer => medicer.User)
                .HasForeignKey(medicer => medicer.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region User Props

            modelBuilder.Entity<User>().
                Property(user => user.Name)
                .IsRequired();

            modelBuilder.Entity<User>().
               Property(user => user.Username)
               .IsRequired();

            modelBuilder.Entity<User>().
              Property(user => user.Password)
              .IsRequired();

            modelBuilder.Entity<User>().
              Property(user => user.Email)
              .IsRequired();

            modelBuilder.Entity<User>().
               Property(user => user.Phone)
               .IsRequired();
            #endregion

            #region seed Data
            //it's not neccessary maint for consulters DEFAULT DATA
            modelBuilder.Entity<Consulter>().HasData(
            new Consulter { Id = 1, Name = "General Medicine Clinic", Description = "Provides primary healthcare services for common illnesses and routine check-ups." },
            new Consulter { Id = 2, Name = "Pediatrics Clinic", Description = "Specializes in medical care for infants, children, and adolescents." },
            new Consulter { Id = 3, Name = "Cardiology Clinic", Description = "Focuses on diagnosing and treating heart - related conditions." }
            );
            #endregion

            #endregion
        }
    }
}
