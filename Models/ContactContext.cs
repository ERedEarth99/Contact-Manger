using Microsoft.EntityFrameworkCore;

namespace Contact_Manger.Models
{
    //UPDATE - JARED STREET:
    // Injected LINQ queries for Http requests into the controllers
    //      - moved ErrorViewModel logic from HomeController into its own model file
    // Added Migrations
    // Couldn't test with no views, but the app runs with no errors!


    /// <summary>
    /// ContactContext - DbContext class for Contact Manager database
    /// 
    /// PURPOSE:
    /// - Represents a session with the database
    /// - Contains DbSet properties for each table (Categories, Contacts)
    /// - Handles database operations through Entity Framework Core
    /// - Seeds initial data when database is created
    /// 
    /// HOW TEAMMATES WILL USE THIS:
    /// - Inject into controllers via Dependency Injection
    /// - Query data: context.Contacts.Include(c => c.Category).ToList()
    /// - Add data: context.Contacts.Add(contact); context.SaveChanges();
    /// - Update data: context.Contacts.Update(contact); context.SaveChanges();
    /// - Delete data: context.Contacts.Remove(contact); context.SaveChanges();
    /// - Find by ID: context.Contacts.Find(id)
    /// 
    /// IMPORTANT: Always call SaveChanges() after Add/Update/Remove operations
    /// </summary>
    public class ContactContext : DbContext
    {
        // Constructor - Accepts DbContextOptions configured in Program.cs
        // Options include connection string and SQL Server provider
        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        { }

        // DbSet Properties - Each represents a table in the database
        // Your teammates will use these to query and modify data

        // Contacts table - stores all contact information
        public DbSet<Contact> Contacts { get; set; } = null!;

        // Categories table - stores contact categories
        public DbSet<Category> Categories { get; set; } = null!;

        // OnModelCreating - Configures database model and seeds initial data
        // Called by EF Core when creating migrations
        // IMPORTANT: Must provide explicit ID values when seeding (even for identity columns)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories using HasData()
            // These 5 categories will be inserted when database is created
            // MUST include CategoryId values even though it's an identity column
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Family" },
                new Category { CategoryId = 2, Name = "Friend" },
                new Category { CategoryId = 3, Name = "Work" },
                new Category { CategoryId = 4, Name = "Business" },
                new Category { CategoryId = 5, Name = "Other" }
            );

            // Seed Sample Contacts using HasData()
            // These 3 contacts provide test data for development
            // MUST include ContactId values even though it's an identity column
            // Foreign key CategoryId links to Categories seeded above
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    Firstname = "John",
                    Lastname = "Doe",
                    Phone = "555-1234",
                    Email = "john.doe@example.com",
                    Organization = "ABC Corporation",
                    CategoryId = 3, // Work
                    DateAdded = new DateTime(2024, 1, 15)
                },
                new Contact
                {
                    ContactId = 2,
                    Firstname = "Jane",
                    Lastname = "Smith",
                    Phone = "555-5678",
                    Email = "jane.smith@example.com",
                    Organization = null, // No organization
                    CategoryId = 2, // Friend
                    DateAdded = new DateTime(2024, 2, 20)
                },
                new Contact
                {
                    ContactId = 3,
                    Firstname = "Bob",
                    Lastname = "Johnson",
                    Phone = "555-9012",
                    Email = "bob.johnson@example.com",
                    Organization = "Johnson Family Services",
                    CategoryId = 1, // Family
                    DateAdded = new DateTime(2024, 3, 10)
                }
            );
        }
    }
}