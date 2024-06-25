using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public IActionResult Index(string time_option, string food, string meat, string vegetable, string type, string search_type, string search, string selected_time, string selected_food, string selected_meat, string selected_vegetable, string selected_type, string selected_search_type, string selected_search, string order_by)
        {
            var recipes = from r in _context.RecipeTables
                          where r.RecipeStatus != "violation"&&r.PublicPrivate!="private"
                          join u in _context.UserInfos on r.UserId equals u.UserId
                          select new {
                              r, u
                          };

            #region 根據 order_by 進行排序
            if (order_by != null)
            {
                switch (order_by)
                {
                    case "Views":
                        recipes = recipes.OrderByDescending(r => r.r.Views);
                        break;
                    case "UserName":
                        recipes = recipes.OrderBy(r => r.u.UserName);
                        break;
                    case "CookingTime":
                        recipes = recipes.OrderBy(r => r.r.CookingTime);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region 根據時間選項過濾
            if (time_option != null && time_option != "all")
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
                    recipes = recipes.Where(r => r.r.CookingTime <= maxTime);
                }
                else
                {
                    recipes = recipes.Where(r => r.r.CookingTime > 60);
                }
            }
            #endregion

            #region 根據食材選項過濾
            if (food != null && food != "all")
            {
                recipes = recipes.Where(r => r.r.ProteinUsed.Contains(food));
            }
            #endregion

            #region 根據餐類選項過濾
            if (meat != null && meat != "all")
            {
                recipes = recipes.Where(r => r.r.MealType.Contains(meat));
            }
            #endregion

            #region 根據膳食選項過濾
            if (vegetable != null && vegetable != "all")
            {
                recipes = recipes.Where(r => r.r.HealthyOptions.Contains(vegetable));
            }
            #endregion

            #region 根據菜式選項過濾
            if (type != null && type != "all")
            {
                recipes = recipes.Where(r => r.r.CuisineStyle.Contains(type));
            }
            #endregion

            #region 根據搜索類型和搜索關鍵字過濾
            if (search != null)
            {
                if (search_type == "recipes")
                {
                    recipes = recipes.Where(r => r.r.RecipeName.Contains(search));
                }
                else if (search_type == "author")
                {
                    recipes = recipes.Where(r => r.u.UserName.Contains(search));
                }
            }
            #endregion

            #region 根據input-hidden時間選項過濾
            if (selected_time != null && selected_time != "all")
            {
                int maxTime = selected_time switch
                {
                    "15min" => 15,
                    "30min" => 30,
                    "60min" => 60,
                    "60min_up" => int.MaxValue,
                    _ => int.MaxValue
                };

                if (selected_time != "60min_up")
                {
                    recipes = recipes.Where(r => r.r.CookingTime <= maxTime);
                }
                else
                {
                    recipes = recipes.Where(r => r.r.CookingTime > 60);
                }
            }
            #endregion

            #region 根據input-hidden食材選項過濾
            if (selected_food != null && selected_food != "all")
            {
                recipes = recipes.Where(r => r.r.ProteinUsed.Contains(selected_food));
            }
            #endregion

            #region 根據input-hidden餐類選項過濾
            if (selected_meat != null && selected_meat != "all")
            {
                recipes = recipes.Where(r => r.r.MealType.Contains(selected_meat));
            }
            #endregion

            #region 根據input-hidden膳食選項過濾
            if (selected_vegetable != null && selected_vegetable != "all")
            {
                recipes = recipes.Where(r => r.r.HealthyOptions.Contains(selected_vegetable));
            }
            #endregion

            #region 根據input-hidden菜式選項過濾
            if (selected_type != null && selected_type != "all")
            {
                recipes = recipes.Where(r => r.r.CuisineStyle.Contains(selected_type));
            }
            #endregion

            #region 根據input-hidden搜索類型和搜索關鍵字過濾
            if (selected_search != null)
            {
                if (selected_search_type == "recipes")
                {
                    recipes = recipes.Where(r => r.r.RecipeName.Contains(selected_search));
                }
                else if (selected_search_type == "author")
                {
                    recipes = recipes.Where(r => r.u.UserName.Contains(selected_search));
                }
            }
            #endregion

            var viewModel = recipes.ToList();
            return View(viewModel);
        }



    }
}
