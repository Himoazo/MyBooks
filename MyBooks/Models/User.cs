﻿using System.ComponentModel.DataAnnotations;

namespace MyBooks.Models;

public class User
{
    public int UserID { get; set; }

    [Required(ErrorMessage = "Namn måste anges")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Namnet måste vara mellan 2 och 100 tecken")]
    [Display(Name = "Namn")]
    public required string User_Name { get; set; }

    [Required(ErrorMessage = "Email måste anges")]
    [EmailAddress(ErrorMessage = "Ogiltig emailadress")]
    [StringLength(150, ErrorMessage = "Email kan inte vara längre än 150 tecken")]
    [Display(Name = "Email")]
    public required string Email { get; set; }

    public List<Borrowing> Borrowings { get; set; } = [];
}
