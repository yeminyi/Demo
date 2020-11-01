using CodeTestDemo.Core.Entities;
using CodeTestDemo.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CodeTestDemo.Infrastructure.Database
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ScoreConfiguration());

        }

        public DbSet<Score> Scores { get; set; }
    
    }
}
