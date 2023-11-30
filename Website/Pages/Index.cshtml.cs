using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaParlor.Data;

namespace Website.Pages
{   public class IndexModel : PageModel
    {
        public Menu? menu;
        public List<IMenuItem>? FilteredPizzas;
        public List<IMenuItem>? FilteredSides;
        public List<IMenuItem>? FilteredDrinks;

        public void OnGet()
        {
            menu = new Menu();
            ApplyFilters();
        }

        public IActionResult OnGetSearch()
        {
            ApplyFilters();
            return Page();
        }

        private void ApplyFilters()
        {
            string searchTerm = Request.Query["searchTerm"];
            string minCalories = Request.Query["minCalories"];
            string maxCalories = Request.Query["maxCalories"];
            string minPrice = Request.Query["minPrice"];
            string maxPrice = Request.Query["maxPrice"];

            FilteredPizzas = menu.GetFilteredPizzas(searchTerm, minCalories, maxCalories, minPrice, maxPrice);
            FilteredSides = menu.GetFilteredSides(searchTerm, minCalories, maxCalories, minPrice, maxPrice);
            FilteredDrinks = menu.GetFilteredDrinks(searchTerm, minCalories, maxCalories, minPrice, maxPrice);
        }
    }
}
