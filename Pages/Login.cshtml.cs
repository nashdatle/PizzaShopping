using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShopping.Data;
using PizzaShopping.Models;

namespace PizzaShopping.Pages
{
    public class LoginModel : PageModel
    {

        private readonly Data.PizzaContext _context;

        public LoginModel(PizzaContext context)
        {
            _context = context;
        }

        [BindProperty] 
        public Account Account { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(account => account.Username == Account.Username && account.Password == Account.Password);
            if (account != null)
            {
                HttpContext.Session.SetString("USERNAME", Account.Username);
                int role = Convert.ToInt32(account.Type);
                HttpContext.Session.SetInt32("ROLE", role);
                return RedirectToPage("./Orders/Index");
            }
            ViewData["msg"] = "Username and Password combination is incorrect!";
            return Page();
        }
    }
}
