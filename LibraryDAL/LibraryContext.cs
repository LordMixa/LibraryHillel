using LibraryDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace LibraryDAL
{
    public class LibraryContext : DbContext//-Project "LibraryDAL" -StartupProject "LibraryDAL"
    {
        public LibraryContext() { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Librarian> Librarian { get; set; }
        public DbSet<Reader> Reader { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<PublisherCodeType> PublisherCodeType { get; set; }
        public DbSet<TakenBook> TakenBook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LibraryHillel;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.Books)
                .WithMany(e => e.Authors);
            modelBuilder.Entity<Book>()
                .HasOne(e => e.PublisherCodeType)
                .WithMany();
            modelBuilder.Entity<Book>()
                .HasOne(e => e.TakenBook)
                .WithOne(e => e.Book)
                .HasForeignKey<TakenBook>();
            modelBuilder.Entity<Reader>()
                .HasOne(e => e.TypeOfDocument)
                .WithMany();
            modelBuilder.Entity<Reader>()
               .HasIndex(e => e.Email)
               .IsUnique();
            modelBuilder.Entity<Reader>()
               .HasIndex(e => e.DocumentNumber)
               .IsUnique();
            modelBuilder.Entity<Book>()
              .HasIndex(e => e.PublisherCode)
              .IsUnique();
            modelBuilder.Entity<Reader>()
                .HasMany(e => e.TakenBook)
                .WithOne(e => e.Reader);
        }
    }
}
