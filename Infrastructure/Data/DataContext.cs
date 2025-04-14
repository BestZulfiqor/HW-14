using System.Data.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<BorrowRecord> BorrowRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(n => n.Author)
            .WithMany(n=>n.Books)
            .HasForeignKey(n => n.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BorrowRecord>()
            .HasOne(n=>n.Book)
            .WithMany()
            .HasForeignKey(n => n.BookId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<BorrowRecord>()
            .HasOne(n => n.Member)
            .WithMany(n => n.BorrowRecords)
            .HasForeignKey(n => n.MemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
