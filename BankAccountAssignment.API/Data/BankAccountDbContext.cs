using Microsoft.EntityFrameworkCore;
namespace BankAccountAssignment.API.Data
{

    public class BankAccountDbContext : DbContext
    {
        public BankAccountDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BankAccount> BankAccounts { get; set; }

    }
}