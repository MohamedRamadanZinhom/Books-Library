using Library2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Book> books { get; set; }
        public DbSet<Borrowed_Book> borrowed_books { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}