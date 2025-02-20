using System.ComponentModel.DataAnnotations;

namespace MyBooks.Models;

public class Book
{
    public int BookID { get; set; }

    [Required(ErrorMessage = "Boknamn måste anges")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Boknamnet måste vara mellan 1 och 100 bokstäver")]
    [Display(Name = "Boknamn")]
    public required string Book_name { get; set; }

    [Required(ErrorMessage = "Genre måste anges")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Genre måste vara mellan 1 och 100 bokstäver")]
    [Display(Name = "Genre")]
    public required string Genre { get; set; }

    [Display(Name = "Tillgänglig?")]
    public bool Available { get; set; } = true;

    [Required(ErrorMessage = "Författare måste anges")]
    [Display(Name = "Författare")]
    public int AuthorID { get; set; }

    [Display(Name = "Författare")]
    public Author? Author { get; set; }
    public List<Borrowing> Borrowings { get; set; } = [];
}
