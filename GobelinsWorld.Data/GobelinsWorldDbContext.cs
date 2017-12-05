namespace GobelinsWorld.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class GobelinsWorldDbContext : IdentityDbContext<User>
    {
        public GobelinsWorldDbContext(DbContextOptions<GobelinsWorldDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Producer> Producers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.Entity<Product>()
                .HasOne(p => p.Producer)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProducerId);

            base.OnModelCreating(builder);
                    }
    }
}
