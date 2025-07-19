using MicroBill.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroBill.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}