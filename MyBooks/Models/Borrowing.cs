using System.ComponentModel.DataAnnotations;

namespace MyBooks.Models;


public class Borrowing
{
    public int BorrowingID { get; set; }

    [Required]
    [Display(Name = "Lånedatum")]
    public DateOnly Borrow_date { get; set; }

    [Display(Name = "Återlämningsdatum")]
    public DateOnly? Return_date { get; set; }

    [Required]
    [Display(Name = "Förfallodatum")]
    public DateOnly Due_date { get; set; }

    [Required(ErrorMessage = "Användare måste anges")]
    public int UserID { get; set; }

    [Display(Name = "Användare")]
    public User User { get; set; } = null!;

    [Required(ErrorMessage = "Bok måste anges")]
    [Display(Name = "Bok")]
    public int BookID { get; set; }

    [Display(Name = "Bok")]
    public Book Book { get; set; } = null!;
}
