namespace MyBooks.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string? User_Name { get; set; }
        public string? Email { get; set; }

        public List<Borrowing> Borrowings { get; set; } = [];
    }
}
