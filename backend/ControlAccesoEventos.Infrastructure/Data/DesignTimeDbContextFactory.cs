using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using System;

namespace ControlAccesoEventos.Infrastructure.Data
{
    
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
           
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                                   ?? Environment.GetEnvironmentVariable("DefaultConnection")
                                   ?? "Server=(localdb)\\mssqllocaldb;Database=ControlAccesoEventosDb;Trusted_Connection=True;";
            builder.UseSqlServer(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}
