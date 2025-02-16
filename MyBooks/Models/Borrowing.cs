namespace MyBooks.Models;

public enum Status
{
    Borrowed,
    Available
}
public class Borrowing
{
    public int BorrowingID { get; set; }
    public Status BookStatus { get; set; }
    public DateTime Borrow_date { get; set; }
    public DateTime? Return_date { get; set; }
    public DateTime Due_date { get; set; }

    public int UserID { get; set; }
    public User User { get; set; } = null!;

    public int BookID { get; set; }
    public Book Book { get; set; } = null!;
}
