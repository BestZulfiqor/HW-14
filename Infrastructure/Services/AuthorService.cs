using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.AuthorDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AuthorService(DataContext context, IMapper mapper) : IAuthorService
{
    public async Task<Response<GetAuthorDto>> AddAuthorAsync(CreateAuthorDto authorDto)
    {
        var author = mapper.Map<Author>(authorDto);

        await context.Authors.AddAsync(author);
        var res = await context.SaveChangesAsync();

        var getAuthorDto = mapper.Map<GetAuthorDto>(author);
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

        var update = mapper.Map<GetAuthorDto>(author);

        return res == 0
            ? new Response<GetAuthorDto>(HttpStatusCode.BadRequest, "Author not added!")
            : new Response<GetAuthorDto>(update);
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
        var getAuthorDto = mapper.Map<GetAuthorDto>(exist);

        return new Response<GetAuthorDto>(getAuthorDto);
    }

    public async Task<Response<List<GetAuthorsWithMostBooksDto>>> GetAuthorsWithMostBooksAsync()
    {
        var maxAuthors = await context.Authors
            .MaxAsync(n => n.Books.Count);
        var authors = await context.Authors
            .Where(n => n.Books.Count == maxAuthors)
            .ToListAsync();
        var data = mapper.Map<List<GetAuthorsWithMostBooksDto>>(authors);
        return new Response<List<GetAuthorsWithMostBooksDto>>(data);
    }
}
