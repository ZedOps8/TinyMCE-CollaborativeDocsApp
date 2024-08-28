using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TextEditor.Models;

namespace TextEditor.Data
{
    // ApplicationDbContext inherits from IdentityDbContext to integrate ASP.NET Core Identity
    // with Entity Framework Core for user authentication and authorization.
    public class ApplicationDbContext : IdentityDbContext
    {
        // Constructor to pass DbContext options to the base class constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet property to manage CRUD operations for Doc entities
        public DbSet<Doc> Docs { get; set; }
    }
}
