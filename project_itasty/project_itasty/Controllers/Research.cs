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

        public IActionResult Index(string time_option, string food,string meat,string vegetable,string type, string search_type, string search, string selected_time_option, string selected_food, string selected_meat, string selected_vegetable, string selected_type, string selected_search_type, string selected_search, string order_by)
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
                                    Collections=r.Collections,
                                    RecipeCover=r.RecipeCover,
                          };

            // 根據 order_by 参数指定的字段进行排序
            if (!string.IsNullOrEmpty(order_by))
            {
                switch (order_by)
                {
                    case "TimesWatched":
                        recipes = recipes.OrderBy(r => r.TimesWatched);
                        break;
                    case "UserName":
                        recipes = recipes.OrderBy(r => r.UserName);
                        break;
                    // 可根据需要添加更多的排序字段
                    default:
                        break;
                }
            }


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

            // 根據時間選項過濾
            if (!string.IsNullOrEmpty(selected_time_option) && selected_time_option != "all")
            {
                int maxTime = selected_time_option switch
                {
                    "15min" => 15,
                    "30min" => 30,
                    "60min" => 60,
                    "60min_up" => int.MaxValue,
                    _ => int.MaxValue
                };

                if (selected_time_option != "60min_up")
                {
                    recipes = recipes.Where(r => r.TimesWatched <= maxTime);
                }
                else
                {
                    recipes = recipes.Where(r => r.TimesWatched > 60);
                }
            }

            // 根據食材選項過濾
            if (!string.IsNullOrEmpty(selected_food) && selected_food != "all")
            {
                recipes = recipes.Where(r => r.ProteinUsed.Contains(selected_food));
            }

            // 根據餐類選項過濾
            if (!string.IsNullOrEmpty(selected_meat) && selected_meat != "all")
            {
                recipes = recipes.Where(r => r.MealType.Contains(selected_meat));
            }

            // 根據膳食選項過濾
            if (!string.IsNullOrEmpty(selected_vegetable) && selected_vegetable != "all")
            {
                recipes = recipes.Where(r => r.HealthyOptions.Contains(selected_vegetable));
            }

            // 根據菜式選項過濾
            if (!string.IsNullOrEmpty(selected_type) && selected_type != "all")
            {
                recipes = recipes.Where(r => r.CuisineStyle.Contains(selected_type));
            }

            // 根據搜索類型和搜索關鍵字過濾
            if (!string.IsNullOrEmpty(selected_search))
            {
                if (selected_search_type == "recipes")
                {
                    recipes = recipes.Where(r => r.Name.Contains(selected_search));
                }
                else if (selected_search_type == "author")
                {
                    recipes = recipes.Where(r => r.UserName.Contains(selected_search));
                }
            }

            var viewModel = recipes.ToList();
            return View(viewModel);
        }


    }
}
