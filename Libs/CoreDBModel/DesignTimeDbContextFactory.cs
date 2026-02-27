using CoreDBModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDBModel
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PrimumContext>
    {
        public PrimumContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PrimumContext>();
            // Временная строка подключения для генерации миграций
            optionsBuilder.UseSqlServer("Server=KOMAROVE;Database=PrimumDB;Trusted_Connection=true;TrustServerCertificate=True;");

            return new PrimumContext(optionsBuilder.Options);
        }
    }
}
