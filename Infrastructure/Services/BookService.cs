using System.Net;
using Domain.Dtos.BookDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookService(DataContext context) : IBookService
{
    public async Task<Response<GetBookDto>> AddBook(CreateBookDto bookDto)
    {
        var book = new Book()
        {
            AuthorId = bookDto.AuthorId,
            Title = bookDto.Title,
            Genre = bookDto.Genre,
            PublishedDate = bookDto.PublishedDate
        };

        await context.Books.AddAsync(book);
        var result = await context.SaveChangesAsync();
        var getBookDto = new GetBookDto()
        {
            Id = book.Id,
            AuthorId = book.AuthorId,
            Genre = book.Genre,
            PublishedDate = book.PublishedDate,
            Title = book.Title
        };

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
        var book = new GetBookDto()
        {
            Id = exist.Id,
            AuthorId = exist.AuthorId,
            Title = exist.Title,
            Genre = exist.Genre,
            PublishedDate = exist.PublishedDate
        };

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

        var book = new GetBookDto()
        {
            Id = exist.Id,
            AuthorId = exist.AuthorId,
            Title = exist.Title,
            Genre = exist.Genre,
            PublishedDate = exist.PublishedDate
        };

        return new Response<GetBookDto>(book);
    }

    public async Task<Response<List<GetBookDto>>> GetBookByAuthor(string name)
    {
        var books = await context.Books
            .Where(n => n.Author.Name == name)
            .Select(n => new GetBookDto()
            {
                Id = n.Id,
                Title = n.Title,
                Genre = n.Genre,
                PublishedDate = n.PublishedDate,
                AuthorId = n.AuthorId
            })
            .ToListAsync();
        return new Response<List<GetBookDto>>(books);
    }

    public async Task<Response<List<GetBookDto>>> GetBookByGenre(string genre)
    {
        var books = await context.Books
            .Where(n => n.Genre == genre)
            .Select(n => new GetBookDto()
            {
                Id = n.Id,
                Title = n.Title,
                Genre = n.Genre,
                PublishedDate = n.PublishedDate,
                AuthorId = n.AuthorId
            })
            .ToListAsync();
        return new Response<List<GetBookDto>>(books);
    }

    public async Task<Response<List<GetBookDto>>> GetRecentlyPublishedBooks(int years)
    {
        var books = await context.Books
            .Where(n => n.PublishedDate.Year >= DateTime.Now.AddYears(-years).Year)
            .Select(n => new GetBookDto()
            {
                Id = n.Id,
                Title = n.Title,
                Genre = n.Genre,
                PublishedDate = n.PublishedDate,
                AuthorId = n.AuthorId
            })
            .ToListAsync();
        return new Response<List<GetBookDto>>(books);
    }
}
