namespace Bookish.Models.Data;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public List<Copy> Copies { get; set; } = [];
}
