using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PizzaShopping.Pages
{
    public class _SampleLayoutModel : PageModel
    {
        public void OnGet()
        {
            String s = "Category";
            ViewData["str"] = s;
        }
    }
}
