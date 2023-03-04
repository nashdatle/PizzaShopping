using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaShopping.Data;
using PizzaShopping.Models;

namespace PizzaShopping.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly PizzaShopping.Data.PizzaContext _context;

        public IndexModel(PizzaShopping.Data.PizzaContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get;set; } = default!;

        [BindProperty]
        public Product Product { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier).ToListAsync();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName");
        }

        // POST
        public async Task<IActionResult> OnPostAsync()
        {
            string action = Request.Form["Action"];
            if (action == "CREATE")
            {
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();
            }
            else if (action == "EDIT")
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == Product.ProductId);
                if (product != null)
                {
                    product.ProductName = Product.ProductName;
                    product.SupplierId = Product.SupplierId;
                    product.CategoryId= Product.CategoryId;
                    product.QuantityPerUnit= Product.QuantityPerUnit;
                    product.UnitPrice = Product.UnitPrice;
                    product.ProductImage= Product.ProductImage;

                    _context.Products.Update(product);

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
