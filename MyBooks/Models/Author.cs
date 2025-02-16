namespace MyBooks.Models;

public class Author
{
    public int AuthorID { get; set; }
    public string? Author_Name { get; set; }

    public List<Book> Books { get; set; } = [];
}
