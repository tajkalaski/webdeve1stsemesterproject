using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RespaunceV2.Infrastructure.Persistence;

namespace Infrastructure.Persistence
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ReportingDbContext>
    {
        public ReportingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReportingDbContext>();
            optionsBuilder.UseSqlServer("Server=LAPTOP-DS3C5LGV\\SQLEXPRESS;Database=RespaunceTest;Trusted_Connection=True;");
            return new ReportingDbContext(optionsBuilder.Options);
        }
    }
}