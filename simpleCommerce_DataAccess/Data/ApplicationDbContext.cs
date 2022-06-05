using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using simpleCommerce_Models;

namespace simpleCommerce_DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Closed because when i add this i cant use id field as Primary Key.
            //Need to find another way to add Unique Constraint to these two column.
            //builder.Entity<ProductTag>().HasKey(x => new { x.ProductId, x.TagId });

            //Added becaouse when i trying to order by products with price taking error.
            //Because sqlite doesnt support decimal types as forums says.
            builder.Entity<Product>()
            .Property(e => e.Price)
            .HasConversion<decimal>();
            base.OnModelCreating(builder);
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductProperty> ProductProperty { get; set; }
        public DbSet<Tag> Tag { get; set; } 
        public DbSet<ProductTag> ProductTag { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<FacultyLogo> FacultyLogo { get; set; }

    }
}
