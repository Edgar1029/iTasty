using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project_itasty.Models;
using System.Collections.Generic;
using System.Diagnostics;


namespace project_itasty.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITastyDbContext _context;

        private readonly ILogger<HomeController> _logger;
        private int month = DateTime.Now.Month;


        public HomeController(ILogger<HomeController> logger, ITastyDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public class MyViewModel
        {
            public List<SeasonalIngredient> Ingredients { get; set; }
            public List<RecipeTable> Recipes { get; set; }
            public Dictionary<string, List<RecipeTable>> SeasonRecipeTable { get; set; }
            public List<RecipeTable> RecipeViews { get; set; }
        }

        public IActionResult Index()
        {
            // 當季食材
            var ingredients = from o in _context.SeasonalIngredients where o.MonthId == 7& o.IsActive==true select o;
            // 熱門食譜
            var recipeViews= from o in _context.RecipeTables  where o.RecipeStatus== "No violation" && o.PublicPrivate == "public" orderby o.Views descending select o;
            // 最新食譜
            var recipes = from o in _context.RecipeTables where o.RecipeStatus == "No violation" && o.PublicPrivate == "public" orderby o.CreatedDate descending select o;
            // 當季食材和食材表格合併
            var seasonIngredients = from r in _context.SeasonalIngredients
						  join u in _context.IngredientsTables on r.CommonName equals u.IngredientsName
						  select new
						  {
							  r,
							  u
						  };
            var ingredientsList = ingredients.ToList(); //當季食材
            List<RecipeTable> recipeViewList =recipeViews.ToList();// 熱門食譜
            List<RecipeTable> recipesList = recipes.ToList();//最新食譜
            var seasontablesList= seasonIngredients.ToList();//season join IngredientsTables




            var seasonRecipeTable = new Dictionary<string, List<RecipeTable>>();

            foreach (var ingredient in seasonIngredients)
            {
                // 尋找包含該食材的所有食譜
                var ingredientRecipes = _context.RecipeTables
                                                .Where(o => o.RecipeId == ingredient.u.RecipeId&& o.RecipeStatus== "No violation" && o.PublicPrivate == "public")
                                                .ToList();

                if (!seasonRecipeTable.ContainsKey(ingredient.r.CommonName))
                {
                    seasonRecipeTable[ingredient.r.CommonName] = new List<RecipeTable>();
                }

                seasonRecipeTable[ingredient.r.CommonName].AddRange(ingredientRecipes);
            }
            //給view的model
            MyViewModel viewModel = new MyViewModel
            {
               Ingredients = ingredientsList,
               Recipes = recipesList,
               RecipeViews = recipeViewList,
                SeasonRecipeTable = seasonRecipeTable
            };

            return View(viewModel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public ActionResult Index(string query, string searchType)//搜尋欄
        {
            var recipes = from r in _context.RecipeTables
                          join u in _context.UserInfos on r.UserId equals u.UserId
                          select new
                          {
                              r,
                              u
                          };
            
                if (searchType == "食譜")
                {
                    recipes = recipes.Where(r => r.r.RecipeName.Contains(query));
                }
                else if (searchType == "作者")
                {
                    recipes = recipes.Where(r => r.u.UserName.Contains(query));
                }
            
            
          
            TempData["SearchResults"] = recipes;
            return Ok(123);
            return RedirectToAction("Index", "Research",TempData);
        }

    }
}
