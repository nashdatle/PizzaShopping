using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShopping.Data;
using PizzaShopping.Models;

namespace PizzaShopping.Pages.Suppliers
{
    public class IndexModel : PageModel
    {
        private readonly PizzaContext _context;

        public IndexModel(PizzaContext context)
        {
            _context = context;
        }

        public IList<Supplier> Suppliers { get;set; } = default!;

        [BindProperty]
        public Supplier Supplier { get; set; }

        // GET
        public async Task OnGetAsync()
        {
            if (_context.Suppliers != null)
            {
                Suppliers = await _context.Suppliers.ToListAsync();
            }
        }

        // POST
        public async Task<IActionResult> OnPostAsync()
        {
            string action = Request.Form["Action"];
            if (action == "CREATE")
            {
                _context.Suppliers.Add(Supplier);
                await _context.SaveChangesAsync();
            } else if (action == "EDIT")
            {
                var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == Supplier.SupplierId);
                if (Supplier != null)
                {
                    supplier.CompanyName = Supplier.CompanyName;
                    supplier.Address = Supplier.Address;
                    supplier.Phone = Supplier.Phone;
                    _context.Suppliers.Update(supplier);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
