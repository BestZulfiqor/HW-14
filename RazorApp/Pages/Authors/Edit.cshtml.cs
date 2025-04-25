using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Entities;
using Infrastructure.Interfaces;
using Domain.Dtos.AuthorDto;

namespace RazorApp.Pages.Authors
{
    public class EditModel(IAuthorService service) : PageModel
    {

        [BindProperty]
        public UpdateAuthorDto UpdateAuthorDto { get; set; } = new();
        public List<string> Messages { get; set; } = [];
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await service.GetAuthorAsync(id);
            if (!response.IsSuccess)
            {
                return StatusCode(response.StatusCode, response.Message!);
            }

            UpdateAuthorDto = new UpdateAuthorDto()
            {
                Id = response.Data!.Id,
                Name = response.Data.Name,
                BirthDate = response.Data.BirthDate
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Messages = ModelState.Values.SelectMany(n => n.Errors).Select(n => n.ErrorMessage).ToList();
                return Page();
            }

            UpdateAuthorDto.BirthDate = UpdateAuthorDto.BirthDate.ToUniversalTime();
            var result = await service.UpdateAuthorAsync(UpdateAuthorDto.Id, UpdateAuthorDto);
            if (!result.IsSuccess)
            {
                Messages.Add("There was an error in updating author");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
