using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project_itasty.Models;
using System.Data.Entity;

namespace project_itasty.Controllers
{
    public class RecipePageController : Controller
    {
		private readonly ITastyDbContext _context;
		public RecipePageController(ITastyDbContext context)
		{
			_context = context;
		}

        [HttpGet]
		public ActionResult Index()
        {
            //var recipe_tables = _context.RecipeTables
            //    .Include(r => r.User)
            //    .Include(r => r.IngredientsTables)
            //    .Include(r => r.StepTables)
            //    .FirstOrDefault(e => e.RecipeId == 1);



            var recipe_tables = _context.RecipeTables
           .Include(r => r.IngredientsTables)
           .Include(r => r.StepTables)
           .Join(_context.UserInfos,
               recipe => recipe.UserId,
               user => user.UserId,
               (recipe, user) => new RecipeWithUser
               {
                   Recipe = recipe,
                   User = user
               })
           .FirstOrDefault(r => r.Recipe.RecipeId == 1);

            if (recipe_tables == null)
            {
                Console.WriteLine("No Recipe found");
            }
            else
            {
                Console.WriteLine(recipe_tables.Recipe.RecipeId);
                Console.WriteLine(recipe_tables.Recipe.UserId);
                Console.WriteLine(recipe_tables.Recipe.RecipeName);
                Console.WriteLine(recipe_tables.Recipe.RecipeIntroduction);
                Console.WriteLine(recipe_tables.Recipe.Views);
                Console.WriteLine(recipe_tables.Recipe.Favorites);
                Console.WriteLine(recipe_tables.Recipe.CreatedDate);
                Console.WriteLine(recipe_tables.Recipe.LastModifiedDate);
                Console.WriteLine(recipe_tables.Recipe.PublicPrivate);
                Console.WriteLine(recipe_tables.Recipe.ProteinUsed);
                Console.WriteLine(recipe_tables.Recipe.MealType);
                Console.WriteLine(recipe_tables.Recipe.CuisineStyle);
                Console.WriteLine(recipe_tables.Recipe.HealthyOptions);
                Console.WriteLine(recipe_tables.Recipe.CookingTime);
                Console.WriteLine(recipe_tables.Recipe.Servings);
                Console.WriteLine(recipe_tables.Recipe.Calories);
                Console.WriteLine(recipe_tables.Recipe.RecipeCoverImage);

                if (recipe_tables.Recipe.IngredientsTables == null)
                {
                    Console.WriteLine("No IngredientsTables info found for this Recipe");
                }
                else
                {
                    Console.WriteLine("IngredientsTables is GOOD ~~~");
                }


                if (recipe_tables.User == null)
                {
                    Console.WriteLine("No User info found for this Recipe");
                }
                else
                {
                    Console.WriteLine($"UserId: {recipe_tables.User.UserId}");
                    Console.WriteLine($"UserName: {recipe_tables.User.UserName}");
                    Console.WriteLine($"UserEmail: {recipe_tables.User.UserPhoto}");
                }

            }



            return View(recipe_tables);
        }


        // GET: RecipeRageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeRageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RecipeRageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecipeRageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RecipeRageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecipeRageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
