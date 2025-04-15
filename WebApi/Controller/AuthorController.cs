using Domain.Dtos;
using Domain.Dtos.AuthorDto;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthorController(IAuthorService service)
{
    [HttpPost]

    public async Task<Response<GetAuthorDto>> AddAuthorAsync(CreateAuthorDto authorDto){
        return await service.AddAuthorAsync(authorDto);
    }  

    [HttpPut("{id:int}")]
    public async Task<Response<GetAuthorDto>> UpdateAuthorAsync(int id, UpdateAuthorDto authorDto){
        return await service.UpdateAuthorAsync(id, authorDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAuthorAsync(int id){
        return await service.DeleteAuthorAsync(id);
    }
    
    [HttpGet("{id:int}")]
    public async Task<Response<GetAuthorDto>> GetAuthorAsync(int id){
        return await service.GetAuthorAsync(id);
    }
    
    [HttpGet("authors-with-most-book")]
    public async Task<Response<List<GetAuthorsWithMostBooksDto>>> GetAuthorsWithMostBooksAsync(){
        return await service.GetAuthorsWithMostBooksAsync();
    }
}
