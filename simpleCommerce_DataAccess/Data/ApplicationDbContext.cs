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
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Picture> Picture { get; set; } = default!;
        public DbSet<Property> Property { get; set; } = default!;
        public DbSet<Tag> Tag { get; set; } = default!;
    }
}
