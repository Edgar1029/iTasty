using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_itasty.Models;
using System.Linq;

namespace project_itasty.Controllers
{
    public class ResearchController : Controller
    {
        private readonly ITastyDbContext _context;

        public ResearchController(ITastyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string time_option, string food,string meat,string vegetable,string type, string search_type, string search)
        {
            var recipes = from r in _context.RecipeTables
                          join u in _context.UserInfos on r.UserId equals u.UserId
                          select new {
                                    Id=r.Id,
                                    Name=r.Name,
                                    UserName=u.UserName,
                                    TimesWatched=r.TimesWatched,
                                    ProteinUsed = r.ProteinUsed,
                                    MealType = r.MealType,
                                    HealthyOptions=r.HealthyOptions,
                                    CuisineStyle = r.CuisineStyle,
                                    RecipeIntroduction=r.RecipeIntroduction,
                          }; // 只選擇 RecipeTable 對象


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
                recipes = recipes.Where(r => r.ProteinUsed.Contains(food));
            }

            // 根據餐類選項過濾
            if (!string.IsNullOrEmpty(meat) && meat != "all")
            {
                recipes = recipes.Where(r => r.MealType.Contains(meat));
            }

            // 根據膳食選項過濾
            if (!string.IsNullOrEmpty(vegetable) && vegetable != "all")
            {
                recipes = recipes.Where(r => r.HealthyOptions.Contains(vegetable));
            }

            // 根據菜式選項過濾
            if (!string.IsNullOrEmpty(type) && type != "all")
            {
                recipes = recipes.Where(r => r.CuisineStyle.Contains(type));
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
                    recipes = recipes.Where(r => r.UserName.Contains(search));
                }
            }

            var viewModel = recipes.ToList();
            return View(viewModel);
        }
    }
}
