using System.ComponentModel.DataAnnotations;

namespace MyBooks.Models;

public class Author
{
    
    public int AuthorID { get; set; }

    [Required(ErrorMessage = "Författarnamn måste anges")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Namnet måste vara mellan 2 och 100 tecken")]
    [Display(Name = "Författare")]
    public required string Author_Name { get; set; }

    public List<Book> Books { get; set; } = [];
}
