namespace Domain.Dtos;

public class GetMemberBorrowDate
{
    public int Id { get; set; }
    public DateTime BorrowDate { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
