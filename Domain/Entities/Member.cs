namespace Domain.Entities;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime MembershipDate { get; set; }

    public List<BorrowRecord> BorrowRecords { get; set; }
}
