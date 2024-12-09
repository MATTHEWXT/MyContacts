using Microsoft.EntityFrameworkCore;
using MyContacts.Domain.Entities;

namespace MyContacts.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<PhoneNumber>()
                .HasKey(pn => pn.Id);

            modelBuilder.Entity<Contact>()
                .HasOne(contact => contact.Category) 
                .WithMany(category => category.Contacts) 
                .HasForeignKey(contact => contact.CategoryId) 
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhoneNumber>()
                .HasOne(pn => pn.Contact)
                .WithMany(contact => contact.PhoneNumbers)
                .HasForeignKey(pn => pn.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhoneNumber>()
                .HasIndex(pn => pn.Number)
                .HasDatabaseName("IX_PhoneNumber_Number");
        }
    }
}
