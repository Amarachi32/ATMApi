using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Domain.DataBase
{
    public class AtmBuilder : IDesignTimeDbContextFactory<AtmDbContext>
    {
        public AtmBuilder() { 
        }
        public AtmDbContext CreateDbContext(string[] args)
        {
           var optionBuilder = new DbContextOptionsBuilder<AtmDbContext>();
            string connectionString = @"Data Source=DESKTOP-J5V3R18\SQLEXPRESS;Initial Catalog=AtmDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionBuilder.UseSqlServer(connectionString);
            return new AtmDbContext(optionBuilder.Options);
        }
    }

}
