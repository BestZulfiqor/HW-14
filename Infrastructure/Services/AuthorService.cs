using System.Net;
using Domain.Dtos;
using Domain.Dtos.AuthorDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AuthorService(DataContext context) : IAuthorService
{
    public async Task<Response<GetAuthorDto>> AddAuthorAsync(CreateAuthorDto authorDto)
    {
        var author = new Author()
        {
            Name = authorDto.Name,
            BirthDate = authorDto.BirthDate,
        };
         
        await context.Authors.AddAsync(author);
        var res = await context.SaveChangesAsync();
        
        var getAuthorDto = new GetAuthorDto()
        {
            Id = author.Id,
            Name = authorDto.Name,
            BirthDate = authorDto.BirthDate,
        };

        return res == 0
            ? new Response<GetAuthorDto>(HttpStatusCode.BadRequest, "Author not added!")
            : new Response<GetAuthorDto>(getAuthorDto);
    }

    public async Task<Response<GetAuthorDto>> UpdateAuthorAsync(int id, UpdateAuthorDto authorDto)
    { 
        var author = await context.Authors.FindAsync(id);
        if (author == null)
        {
            return new Response<GetAuthorDto>(HttpStatusCode.NotFound, "Author not found");
        }
        author.Name = authorDto.Name;
        author.BirthDate = authorDto.BirthDate;
        
        var res = await context.SaveChangesAsync();

        var resDto = new GetAuthorDto()
        {
            Id = author.Id,
            Name = authorDto.Name,
            BirthDate = authorDto.BirthDate,
        };
        
        return res == 0 
            ? new Response<GetAuthorDto>(HttpStatusCode.BadRequest, "Author not added!")
            : new Response<GetAuthorDto>(resDto);
    }

    public async Task<Response<string>> DeleteAuthorAsync(int id)
    {
        var exist = await context.Authors.FindAsync(id);
        
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Author not found");
        }

        context.Authors.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Author not deleted!")
            : new Response<string>("Author deleted!");
        
    }

    public async Task<Response<GetAuthorDto>> GetAuthorAsync(int id)
    {
        var exist = await context.Authors.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetAuthorDto>(HttpStatusCode.NotFound, "Author not found!");
        }

        var book = new GetAuthorDto()
        {
            Id = exist.Id,
            Name = exist.Name,
            BirthDate = exist.BirthDate,
        };

        return new Response<GetAuthorDto>(book);
    }

    public async Task<Response<List<GetAuthorsWithMostBooksDto>>> GetAuthorsWithMostBooksAsync(int count)
    {
        var authors = await context.Authors.Select(a => new GetAuthorsWithMostBooksDto()
            {
                Id = a.Id,
                Name = a.Name,
                BirthDate = a.BirthDate,
                Count = a.Books.Count,
            }
        ).ToListAsync();

        return new Response<List<GetAuthorsWithMostBooksDto>>(authors);
    }
}
