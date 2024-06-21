using Microsoft.AspNetCore.Mvc;
using project_itasty.Models;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

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

		[HttpGet]
		public IActionResult Create()
		{
			int? userid_int = HttpContext.Session.GetInt32("userId");
			var user = _context.UserInfos.Find(userid_int);

			return View(user);
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

			#region 將RecipeId和UserId放入ingredients_table和step_table

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

			return RedirectToAction("Index", "RecipePage", new { recipe_id = recipe_table.RecipeId });

		}


		[HttpGet]
		public IActionResult Edit_recipe(int recipeId)
		{
			int? userid_int = HttpContext.Session.GetInt32("userId");

			var recipe_table = _context.RecipeTables.Find(recipeId);



			var ingredients_table = _context.IngredientsTables
									.Where(i => i.RecipeId == recipeId)
									.Select(i => new IngredientsForEdit
									{
										IngredientsTableId = i.Id,
										IngredientUserId = i.UserId,
										IngredientRecipeId = i.RecipeId,
										TitleName = i.TitleName,
										TitleId = i.TitleId,
										IngredientsId = i.IngredientsId,
										IngredientsName = i.IngredientsName,
										IngredientsNumber = i.IngredientsNumber,
										IngredientsUnit = i.IngredientsUnit,
										IngredientKcalg = i.Kcalg
									})
									.ToList();

			var step_table = _context.StepTables
							 .Where(s => s.RecipeId == recipeId)
							 .Select(s => new StepForEdit
							 {
								 StepId = s.Id,
								 StepRecipeId = s.RecipeId,
								 StepText = s.StepText,
								 StepImg = s.StepImg,
								 StepBase64 = s.StepBase64,
							 })
							 .ToList();

			var user = _context.UserInfos.Find(recipe_table.UserId);

			var edit_recipe = new RecipeDetailsView
			{
				User = user,
				Recipe = recipe_table,
				IngredientsEdit = ingredients_table,
				StepEdit = step_table,
			};
			return View(edit_recipe);
		}

		[HttpPost]
		public async Task<IActionResult> Edit_recipe(RecipeTable recipe_table, List<IngredientsForEdit> ingredients_table, List<StepForEdit> step_table)
		{
			int? userid_int = HttpContext.Session.GetInt32("userId");


			var old_recipe = _context.RecipeTables.Find(recipe_table.RecipeId);
			var step = _context.StepTables.Find(recipe_table.RecipeId);

			if (!string.IsNullOrEmpty(recipe_table.RecipeCoverBase64))
			{
				if (Is_base64(recipe_table.RecipeCoverBase64))
				{
					recipe_table.RecipeCoverImage = Convert.FromBase64String(recipe_table.RecipeCoverBase64);
				}
			}


			if (recipe_table.RecipeName != old_recipe.RecipeName || recipe_table.RecipeCoverImage != old_recipe.RecipeCoverImage || recipe_table.RecipeIntroduction != old_recipe.RecipeIntroduction
				|| recipe_table.PublicPrivate != old_recipe.PublicPrivate || recipe_table.ProteinUsed != old_recipe.ProteinUsed || recipe_table.MealType != old_recipe.MealType 
				|| recipe_table.CuisineStyle != old_recipe.CuisineStyle || recipe_table.HealthyOptions != old_recipe.HealthyOptions || recipe_table.CookingTime != old_recipe.CookingTime 
				|| recipe_table.Servings != old_recipe.Servings || recipe_table.Calories != old_recipe.Calories)
			{
				old_recipe.RecipeName = recipe_table.RecipeName;
				old_recipe.RecipeCoverImage = recipe_table.RecipeCoverImage;
				old_recipe.RecipeIntroduction = recipe_table.RecipeIntroduction;
				old_recipe.PublicPrivate = recipe_table.PublicPrivate;
				old_recipe.ProteinUsed = recipe_table.ProteinUsed;
				old_recipe.MealType = recipe_table.MealType;
				old_recipe.CuisineStyle = recipe_table.CuisineStyle;
				old_recipe.HealthyOptions = recipe_table.HealthyOptions;
				old_recipe.CookingTime = recipe_table.CookingTime;
				old_recipe.Servings = recipe_table.Servings;
				old_recipe.Calories = recipe_table.Calories;
				old_recipe.LastModifiedDate = DateTime.Now;
			}

			_context.Entry(old_recipe).State = EntityState.Modified;


			foreach (var ing in ingredients_table)
			{

				Console.WriteLine("=================================================================");
				Console.WriteLine("超級新資料的Id : " + ing.IngredientsTableId);
				Console.WriteLine("=================================================================");

				var ingredients = await _context.IngredientsTables.FindAsync(ing.IngredientsTableId);
				if (ingredients != null)
				{

					Console.WriteLine("=================================================================");
					Console.WriteLine("新資料的Id : " + ing.IngredientsTableId);
					Console.WriteLine("舊資料的Id : " + ingredients.Id);
					Console.WriteLine("舊資料的RecipeId : " + ingredients.RecipeId);
					Console.WriteLine("=================================================================");

					if (ingredients.TitleName != ing.TitleName || ingredients.IngredientsId != ing.IngredientsId || ingredients.IngredientsName != ing.IngredientsName
						 || ingredients.IngredientsNumber != ing.IngredientsNumber || ingredients.IngredientsUnit != ing.IngredientsUnit || ingredients.Kcalg != ing.IngredientKcalg)
					{
						ingredients.Id = ing.IngredientsTableId;
						ingredients.RecipeId = ing.IngredientRecipeId;
						ingredients.UserId = ing.IngredientUserId;
						ingredients.TitleName = ing.TitleName;
						ingredients.TitleId = ing.TitleId;
						ingredients.IngredientsId = ing.IngredientsId;
						ingredients.IngredientsName = ing.IngredientsName;
						ingredients.IngredientsNumber = ing.IngredientsNumber;
						ingredients.IngredientsUnit = ing.IngredientsUnit;
						ingredients.Kcalg = ing.IngredientKcalg;
						_context.Entry(ingredients).State = EntityState.Modified;
					}
				}
				else
				{
					Console.WriteLine("=================================================================");
					Console.WriteLine("新資料的RecipeId : " + ing.IngredientRecipeId);
					Console.WriteLine("=================================================================");
					var new_Ingredient = new IngredientsTable
					{
						UserId = ing.IngredientUserId,
						RecipeId = ing.IngredientRecipeId,
						TitleName = ing.TitleName,
						TitleId = ing.TitleId,
						IngredientsId = ing.IngredientsId,
						IngredientsName = ing.IngredientsName,
						IngredientsNumber = ing.IngredientsNumber,
						IngredientsUnit = ing.IngredientsUnit,
						Kcalg = ing.IngredientKcalg
					};
					_context.IngredientsTables.Add(new_Ingredient);
				}

			}


			foreach (var steps in step_table)
			{
				var old_steps = await _context.StepTables.FindAsync(steps.StepId);
				if (!string.IsNullOrEmpty(steps.StepBase64))
				{
					if (Is_base64(steps.StepBase64))
					{

						steps.StepImg = Convert.FromBase64String(steps.StepBase64);

					}
				}

				if (old_steps != null)
				{
					if(old_steps.StepText != steps.StepText || old_steps.StepImg != steps.StepImg)
					{
						old_steps.StepText = steps.StepText;
						old_steps.StepImg = steps.StepImg;
						_context.Entry(old_steps).State = EntityState.Modified;
					}
				}
				else
				{
					var new_step = new StepTable
					{
						RecipeId = steps.StepRecipeId,
						StepText = steps.StepText,
						StepImg = steps.StepImg,
					};
					_context.StepTables.Add(new_step);
				}

			}

			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "RecipePage", new { recipe_id = recipe_table.RecipeId });
		}


		//base64格式判斷
		public static bool Is_base64(string? base64)
		{
			return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,2}$", RegexOptions.None);
		}

	}
}


