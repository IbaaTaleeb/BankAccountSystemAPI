using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace BankAccountAssignment.API.Data
{

    public class BankAccountDbContext : DbContext
    {
        public BankAccountDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasSequence("BankAccountSequence").HasMin(0);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> GetBankAccountSeqNumber() {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int) {
                Direction = System.Data.ParameterDirection.Output
            };
            string query = "SELECT @result = (NEXT VALUE FOR BankAccountSequence)";
            await Database.ExecuteSqlRawAsync(query, result);

            return (int)result.Value;
        }

    }
}