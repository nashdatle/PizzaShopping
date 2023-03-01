using PizzaShopping.Models;

namespace PizzaShopping.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PizzaContext context) { 
            if (context.Categories.Any())
            {
                return;
            }

            if (context.Products.Any())
            {
                return;
            }

            List<Category> categories = new List<Category>
                {
                    new Category { CategoryName = "Meat", Description = "For meat eaters" },
                    new Category { CategoryName = "Vegan", Description = "For meat haters" }
                };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}
