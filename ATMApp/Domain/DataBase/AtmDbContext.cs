using ATMApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ATMApp.Domain.DataBase
{
    public class AtmDbContext : DbContext
    {
        public AtmDbContext(DbContextOptions<AtmDbContext> options) : base(options)
        {

        }
        public virtual DbSet<UserAccount> Users { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<InternalTransfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

/*            modelBuilder.Entity<UserAccount>(e =>
                {
                    e.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(20);
                    e.HasIndex(e => new { e.FullName, e.AccountBalance },
                        $"IX_Unique_{nameof(UserAccount.FullName)} = {nameof(UserAccount.AccountBalance)}"
                    );
                });*/



            base.OnModelCreating(modelBuilder);
        }
    }
}
