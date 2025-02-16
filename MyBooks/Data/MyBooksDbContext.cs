using Microsoft.EntityFrameworkCore;
using MyBooks.Models;

namespace MyBooks.Data;

public class MyBooksDbContext: DbContext
{
    public MyBooksDbContext(DbContextOptions<MyBooksDbContext> options) : base(options)
    {
        
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }
    public DbSet<User> Users { get; set; }
}
