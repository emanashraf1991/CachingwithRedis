using CachingwithRedis.Models;
using Microsoft.EntityFrameworkCore;

namespace CachingwithRedis.Data{
    public class DatabaseContext : DbContext{
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
            
        }
        public DbSet<Member> Members { get; set; }
    }
}