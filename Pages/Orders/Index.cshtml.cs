using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShopping.Data;
using PizzaShopping.Models;

namespace PizzaShopping.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly PizzaShopping.Data.PizzaContext _context;

        public IndexModel(PizzaShopping.Data.PizzaContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get;set; } = default!;
        
        [BindProperty]
        public Order Order { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Orders = await _context.Orders
                .Include(o => o.Customer).ToListAsync();
            }
            ViewData["Products"] = _context.Products.ToList();
        }
    }
}
