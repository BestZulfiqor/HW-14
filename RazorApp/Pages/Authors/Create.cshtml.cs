using Domain.Dtos.AuthorDto;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.Authors;

public class Create(IAuthorService service) : PageModel
{
    [BindProperty]
    public CreateAuthorDto CreateAuthorDto { get; set; } = new();
    public List<string> Messages { get; set; } = [];

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Messages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            return Page();
        }

        CreateAuthorDto.BirthDate = CreateAuthorDto.BirthDate.ToUniversalTime();
        var response = await service.AddAuthorAsync(CreateAuthorDto);
        if (response.IsSuccess)
        {
            return Redirect("/Authors/Index");
        }

        Messages.Add(response.Message!);
        return Page();
    }
}
