using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShopping.Data;
using PizzaShopping.Models;

namespace PizzaShopping.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly PizzaShopping.Data.PizzaContext _context;

        public IndexModel(PizzaShopping.Data.PizzaContext context)
        {
            _context = context;
        }

        public IList<Account> Accounts { get;set; } = default!;
        [BindProperty] 
        public Account Account { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Accounts != null)
            {
                Accounts = await _context.Accounts.ToListAsync();
            }
        }

        // POST
        public async Task<IActionResult> OnPostAsync()
        {
            string action = Request.Form["Action"];
            if (action == "CREATE")
            {
                _context.Accounts.Add(Account);
                await _context.SaveChangesAsync();
            }
            else if (action == "EDIT")
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == Account.AccountId);
                if (account != null)
                {
                    account.Username = Account.Username;
                    account.Password = Account.Password;
                    account.Type= Account.Type;
                    _context.Accounts.Update(account);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
