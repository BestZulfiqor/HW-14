using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorApp.Pages.Authors
{
    public class Delete(DataContext context) : PageModel
    {

        [BindProperty]
        public Author? Author { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author = await context.Authors.FirstOrDefaultAsync(m => m.Id == id);

            if (Author == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author = await context.Authors.FindAsync(id);

            if (Author != null)
            {
                context.Authors.Remove(Author);
                await context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
