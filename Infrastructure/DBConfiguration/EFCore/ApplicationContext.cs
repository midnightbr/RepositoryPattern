using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DBConfiguration.EFCore
{
    public class ApplicationContext : DbContext
    {
        // Criando DatabaseContext sem Dependecy Injection
        public ApplicationContext(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(DataBaseConnection.Configuration.GetConnectionString("DefaultConnection"));
        }

        // Criando DatabaseContext com Dependecy Injection
        public ApplicationContext(DbContextOptions<ApplicationContext> optionsBuilder) : base(optionsBuilder)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<TaskToDo> TaskToDo { get; set; }
    }
}
