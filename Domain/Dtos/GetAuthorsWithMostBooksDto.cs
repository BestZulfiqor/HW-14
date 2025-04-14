using Domain.Dtos.AuthorDto;

namespace Domain.Dtos;

public class GetAuthorsWithMostBooksDto : GetAuthorDto
{
    public int Count { get; set; }
}
