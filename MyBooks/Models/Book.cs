namespace MyBooks.Models;

public class Book
{
    public int BookID { get; set; }
    public string? Book_name { get; set; }
    public string? Genre { get; set; }

    public int AuthorID { get; set; }
    public Author Author { get; set; } = null!;
    public List<Borrowing> Borrowings { get; set; } = [];
}
