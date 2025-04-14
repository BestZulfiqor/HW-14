using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class BorrowRecord
{
    [Key]
    public int Id { get; set; }
    public int MemberId { get; set; }
    public int BookId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime ReturnDate { get; set; }

    public Member Member { get; set; }
    public Book Book { get; set; }
}
