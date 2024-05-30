using Microsoft.AspNetCore.Mvc;
using project_itasty.Models;

namespace project_itasty.Controllers
{
    public class ResearchController : Controller
    {
        private readonly ITastyDbContext _context;

        public ResearchController(ITastyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string time_option, string food, string search_type, string search)
        {
            var recipes = _context.RecipeTables.AsQueryable();

            // 根據時間選項過濾
            if (!string.IsNullOrEmpty(time_option) && time_option != "all")
            {
                int maxTime = time_option switch
                {
                    "15min" => 15,
                    "30min" => 30,
                    "60min" => 60,
                    "60min_up" => int.MaxValue,
                    _ => int.MaxValue
                };

                if (time_option != "60min_up")
                {
                    recipes = recipes.Where(r => r.TimesWatched <= maxTime);
                }
                else
                {
                    recipes = recipes.Where(r => r.TimesWatched > 60);
                }
            }

            // 根據食材選項過濾
            if (!string.IsNullOrEmpty(food) && food != "all")
            {
                recipes = recipes.Where(r => r.MealType.Contains(food));
            }

            // 根據搜索類型和搜索關鍵字過濾
            if (!string.IsNullOrEmpty(search))
            {
                if (search_type == "recipes")
                {
                    recipes = recipes.Where(r => r.Name.Contains(search));
                }
                else if (search_type == "author")
                {
                    recipes = recipes.Where(r => r.Name.Contains(search));
                }
            }

            var viewModel = recipes.ToList();
            return View(viewModel);
        }
    }
}
