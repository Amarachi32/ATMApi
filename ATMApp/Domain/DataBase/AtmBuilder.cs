using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ATMApp.Domain.DataBase
{
    public class AtmBuilder : IDesignTimeDbContextFactory<AtmDbContext>
    {

        public AtmBuilder()
        {
        }
        public AtmDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<AtmDbContext>();
            string connectionString = ConfigurationManager.AppSettings["DatabaseConnectionString"];
            optionBuilder.UseSqlServer(connectionString);
            return new AtmDbContext(optionBuilder.Options);
        }
    }

}
