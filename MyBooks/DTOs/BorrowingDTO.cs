using MyBooks.Models;
using System.ComponentModel.DataAnnotations;
namespace MyBooks.DTOs;

public class BorrowingDTO
{

    [Required(ErrorMessage = "Användare måste anges")]
    [Display(Name = "Användare")]
    public int UserID { get; set; }

    [Required(ErrorMessage = "Bok måste anges")]
    [Display(Name = "Bok")]
    public int BookID { get; set; }

    [Required(ErrorMessage = "Lånedatum måste anges")]
    [Display(Name = "Lånedatum")]
    public DateOnly Borrow_date { get; set; }
}


public class ReturnBookDTO
{
    [Required(ErrorMessage = "Låne-ID måste anges")]
    public int BorrowingID { get; set; }

    [Required(ErrorMessage = "Bok måste anges")]
    [Display(Name = "Bok")]
    public int BookID { get; set; }

    [Required(ErrorMessage = "Återlämningsdatum måste anges")]
    [Display(Name = "Återlämningsdatum")]
    public DateOnly? Return_date { get; set; }
}