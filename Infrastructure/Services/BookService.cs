using System.Net;
using AutoMapper;
using Domain.Dtos.BookDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookService(DataContext context, IMapper mapper) : IBookService
{
    public async Task<Response<GetBookDto>> AddBook(CreateBookDto bookDto)
    {
        var book = mapper.Map<Book>(bookDto);

        await context.Books.AddAsync(book);
        var result = await context.SaveChangesAsync();
        var getBookDto = mapper.Map<GetBookDto>(book);

        return result == 0
            ? new Response<GetBookDto>(HttpStatusCode.BadRequest, "Book not added!")
            : new Response<GetBookDto>(getBookDto);
    }

    public async Task<Response<string>> DeleteBook(int id)
    {
        var exist = await context.Books.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Book not found");
        }

        context.Books.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Book not deleted!")
            : new Response<string>("Book deleted!");
    }

    public async Task<Response<GetBookDto>> UpdateBook(int id, UpdateBookDto bookDto)
    {
        var exist = await context.Books.FindAsync(bookDto);
        if (exist == null)
        {
            return new Response<GetBookDto>(HttpStatusCode.NotFound, "Book not found");
        }

        exist.AuthorId = bookDto.AuthorId;
        exist.Genre = bookDto.Genre;
        exist.PublishedDate = bookDto.PublishedDate;
        exist.Title = bookDto.Title;
        var result = await context.SaveChangesAsync();
        var book = mapper.Map<GetBookDto>(exist);

        return result == 0
            ? new Response<GetBookDto>(HttpStatusCode.BadRequest, "Book not updated")
            : new Response<GetBookDto>(book);
    }
    public async Task<Response<GetBookDto>> GetBook(int id)
    {
        var exist = await context.Books.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetBookDto>(HttpStatusCode.NotFound, "Book not found!");
        }
        var book = mapper.Map<GetBookDto>(exist);

        return new Response<GetBookDto>(book);
    }

    public async Task<Response<List<GetBookDto>>> GetBookByAuthor(string name)
    {
        var books = await context.Books
            .Where(n => n.Author.Name == name)
            .ToListAsync();
        
        var data = mapper.Map<List<GetBookDto>>(books);
        return new Response<List<GetBookDto>>(data);
    }

    public async Task<Response<List<GetBookDto>>> GetBookByGenre(string genre)
    {
        var books = await context.Books
            .Where(n => n.Genre == genre)
            .ToListAsync();
        var bookDto = mapper.Map<List<GetBookDto>>(books);
        return new Response<List<GetBookDto>>(bookDto);
    }

    public async Task<Response<List<GetBookDto>>> GetRecentlyPublishedBooks(int years)
    {
        var books = await context.Books
            .Where(n => n.PublishedDate.Year >= DateTime.Now.AddYears(-years).Year)
            .ToListAsync();

        var data = mapper.Map<List<GetBookDto>>(books);
        return new Response<List<GetBookDto>>(data);
    }
}
