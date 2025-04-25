using Domain.Dtos.AuthorDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Authors;

public class Index(IAuthorService service) : PageModel
{
    public List<GetAuthorDto> Authors { get; set; }
    public List<string> Messages { get; set; }
    
    public async Task OnGetAsync(){
        var response = await service.GetAuthorsAsync();
        if (!response.IsSuccess)
        {
            Messages.Add("Somethings went wrong");
            return;
        }

        Authors = response.Data!;
    }
}
