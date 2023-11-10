using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Offset_vs_Keyset_Pagination
{
    internal class DataContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSqlLocalDb;Database=MyDb;Trusted_Connection=True");
        }
    }

    public class Entity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
