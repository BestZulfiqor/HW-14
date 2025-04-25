using Domain.Dtos;
using Domain.Dtos.AuthorDto;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IAuthorService
{
    Task<Response<List<GetAuthorDto>>> GetAuthorsAsync();
    Task<Response<GetAuthorDto>> AddAuthorAsync(CreateAuthorDto authorDto);
    Task<Response<GetAuthorDto>> UpdateAuthorAsync(int id, UpdateAuthorDto authorDto);
    Task<Response<string>> DeleteAuthorAsync(int id);
    Task<Response<GetAuthorDto>> GetAuthorAsync(int id);
    Task<Response<List<GetAuthorsWithMostBooksDto>>> GetAuthorsWithMostBooksAsync();
}
