namespace Domain.Dtos.BookDto;

public class CreateBookDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime PublishedDate { get; set; }
    public int AuthorId { get; set; }
}
