using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShopping.Data;
using PizzaShopping.Models;

namespace PizzaShopping.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly PizzaShopping.Data.PizzaContext _context;

        public IndexModel(PizzaShopping.Data.PizzaContext context)
        {
            _context = context;
        }

        public IList<Customer> Customers { get;set; } = default!;

        [BindProperty] 
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            if (type != 1)
            {
                return Unauthorized();
            }

            if (_context.Customers != null)
            {
                Customers = await _context.Customers.ToListAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            if (type == -1)
            {
                return Unauthorized();
            }

            string action = Request.Form["Action"];
            if (action == "CREATE")
            {
                _context.Customers.Add(Customer);
                await _context.SaveChangesAsync();
            }
            else if (action == "EDIT")
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == Customer.CustomerId);
                if (customer != null)
                {
                    customer.Password = Customer.Password;
                    customer.ContactName = Customer.ContactName;
                    customer.Address = Customer.Address;
                    customer.Phone = Customer.Phone;
                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
