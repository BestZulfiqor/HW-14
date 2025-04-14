using Domain.Dtos.BookDto;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IBookService
{
    Task<Response<GetBookDto>> AddBook(CreateBookDto bookDto);
    Task<Response<GetBookDto>> UpdateBook(int id, UpdateBookDto bookDto);
    Task<Response<string>> DeleteBook(int id);
    Task<Response<GetBookDto>> GetBook(int id);
    Task<Response<List<GetBookDto>>> GetBookByAuthor(string name);
    Task<Response<List<GetBookDto>>> GetBookByGenre(string genre);
    Task<Response<List<GetBookDto>>> GetRecentlyPublishedBooks(int years);
}
