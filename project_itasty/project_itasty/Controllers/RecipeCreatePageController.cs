using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using project_itasty.Models;
using System.Drawing;
using System.Text.RegularExpressions;

namespace project_itasty.Controllers
{
	public class RecipeCreatePageController : Controller
	{
		private readonly ITastyDbContext _context;
		public RecipeCreatePageController(ITastyDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Create_select(string ingredient_select)
		{
			var query = _context.IngredientDetails
								.Where(n => n.IngredientName.Contains(ingredient_select))
								.ToList();


			return PartialView("_Create_select", query);
		}
		[HttpPost]
		public IActionResult Create(RecipeTable recipe_table, List<IngredientsTable> ingredients_table, List<StepTable> step_table)
		{
			int? userid_int = HttpContext.Session.GetInt32("userId");

			#region recipe_table上傳

			recipe_table.UserId = (int)userid_int;			
			recipe_table.Views = 0;
			recipe_table.Favorites = 0;
			recipe_table.RecipeStatus = "recipeStatus";
			recipe_table.CreatedDate = DateTime.Now;
			recipe_table.LastModifiedDate = DateTime.Now;

			if (!string.IsNullOrEmpty(recipe_table.RecipeCoverBase64))
			{
				if (Is_base64(recipe_table.RecipeCoverBase64))
				{
					recipe_table.RecipeCoverImage = Convert.FromBase64String(recipe_table.RecipeCoverBase64);
				}
			}

			_context.RecipeTables.Add(recipe_table);
			_context.SaveChanges();

			#endregion

			#region 將RecipeId和UserId放入recipe_table和step_table

			int newRecipeId = recipe_table.RecipeId;
			for (int i = 0; i < ingredients_table.Count; i++)
			{
				ingredients_table[i].RecipeId = newRecipeId;
				ingredients_table[i].UserId = (int)userid_int;
			}

			for (int i = 0; i < step_table.Count; i++)
			{
				step_table[i].RecipeId = newRecipeId;
			}

			#endregion

			#region 將ingredients_table上傳
			int? new_title_id = null;
			foreach (var ingredient in ingredients_table)
			{
				if (ingredient.TitleName != null)
				{
					_context.IngredientsTables.Add(ingredient);
					_context.SaveChanges();
					new_title_id = ingredient.Id;
				}
				else
				{
					ingredient.TitleId = new_title_id;
					_context.IngredientsTables.Add(ingredient);
					_context.SaveChanges();
				}
			}

			#endregion

			#region 將step_table上傳

			foreach (var step in step_table)
			{
				if (!string.IsNullOrEmpty(step.StepBase64))
				{
					if (Is_base64(step.StepBase64))
					{

						step.StepImg = Convert.FromBase64String(step.StepBase64);

					}
				}
			}

			_context.StepTables.AddRange(step_table);
			_context.SaveChanges();

			#endregion
			
			return Redirect("/RecipePage/Index");

		}


		[HttpGet]
		public IActionResult Edit_recipe(int recipeId)
		{
			int? userid_int = HttpContext.Session.GetInt32("userId");

			var recipe_table = _context.RecipeTables.Find(recipeId);
							   
							   

			var ingredients_table = _context.IngredientsTables
									.Where(i => i.RecipeId == recipeId)
									.ToList();

			var step_table = _context.StepTables
							 .Where(s => s.RecipeId == recipeId)
							 .ToList();

			var user = _context.UserInfos.Find(recipe_table.UserId);

			var edit_recipe = new RecipeDetailsView
			{
				User = user,
				Recipe = recipe_table,
				IngredientsTables = ingredients_table,
				StepTables = step_table
			};
			return View(edit_recipe);
		}

		[HttpPost]
		public IActionResult Edit_recipe(RecipeTable recipe_table, List<IngredientsTable> ingredients_table, List<StepTable> step_table)
		{
			return RedirectToAction("/RecipePage/Index", new { recipe_id  = 3});
		}


		//base64格式判斷
		public static bool Is_base64(string? base64)
		{
			return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,2}$", RegexOptions.None);
		}

	}
}


