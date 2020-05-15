using Microsoft.EntityFrameworkCore;
using webCurrencyPurse.Models;

namespace webCurrencyPurse.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Purse> Purses { get; set; }
        public DbSet<Bill> Bills { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}