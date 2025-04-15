using Domain.Dtos.BookDto;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService service)
{
    [HttpPost]
    public async Task<Response<GetBookDto>> AddBook(CreateBookDto bookDto)
    {
        return await service.AddBook(bookDto);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetBookDto>> UpdateBook(int id, UpdateBookDto bookDto)
    {
        return await service.UpdateBook(id, bookDto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteBook(int id)
    {
        return await service.DeleteBook(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetBookDto>> GetBook(int id)
    {
        return await service.GetBook(id);
    }
    [HttpGet("name/{name}")]
    public async Task<Response<List<GetBookDto>>> GetBookByAuthor(string name)
    {
        return await service.GetBookByAuthor(name);
    }
    [HttpGet("genre/{genre}")]
    public async Task<Response<List<GetBookDto>>> GetBookByGenre(string genre)
    {
        return await service.GetBookByGenre(genre);
    }
    [HttpGet("years/{years:int}")]
    public async Task<Response<List<GetBookDto>>> GetRecentlyPublishedBooks(int years)
    {
        return await service.GetRecentlyPublishedBooks(years);
    }
}
