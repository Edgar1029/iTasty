using Microsoft.AspNetCore.Mvc;
using project_itasty.Models;
using System.Data.Entity;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace project_itasty.Controllers
{
	public class RecipePageController : Controller
	{
		private readonly ITastyDbContext _context;
		public RecipePageController(ITastyDbContext context)
		{
			_context = context;
		}

		[HttpPost("recipe")]
		public ActionResult Index()
		{

			int recipe_id = int.Parse(Request.Form["recipe_id"]);

			HttpContext.Session.SetInt32("RecipeId", recipe_id);

			int? userid_int = HttpContext.Session.GetInt32("userId");

			var recipe = _context.RecipeTables
			 .Where(r => r.RecipeId == recipe_id)
			 .FirstOrDefault();


			if (userid_int.HasValue && userid_int == recipe.UserId)
			{
				return RedirectToAction("author_page", "RecipePage");
			}
			else if (userid_int.HasValue)
			{
				return RedirectToAction("user_page", "RecipePage");
			}
			else
			{
				return RedirectToAction("guest_page", "RecipePage");
			}
		}

		[HttpGet("recipe/index/{recipe_id?}")]
		public ActionResult Recipe_return(int? recipe_id)
		{
			Console.WriteLine("有進來");
			if (recipe_id.HasValue)
			{
				HttpContext.Session.SetInt32("RecipeId", (int)recipe_id);

				int? userid_int = HttpContext.Session.GetInt32("userId");

				return RedirectToAction("author_page", "RecipePage");
			}
			else
			{
				return Redirect("Home/Index");
			}

		}


		public IActionResult user_page(RecipeDetailsView recipe_details)
		{

			int? recipe_id = HttpContext.Session.GetInt32("RecipeId");

			int? userid_int = HttpContext.Session.GetInt32("userId");



			var recipe_model = GetRecipeDetailsView(recipe_id, userid_int);

			return View(recipe_model);

		}
		public IActionResult author_page(RecipeDetailsView recipe_details)
		{

			var recipe_id = HttpContext.Session.GetInt32("RecipeId");
			int? userid_int = HttpContext.Session.GetInt32("userId");

			var recipe_model = GetRecipeDetailsView(recipe_id, userid_int);

			return View(recipe_model);

		}
		public IActionResult guest_page(RecipeDetailsView recipe_details)
		{

			var recipe_id = HttpContext.Session.GetInt32("RecipeId");

			int? userid_int = HttpContext.Session.GetInt32("userId");

			var recipe_model = GetRecipeDetailsView(recipe_id, userid_int);

			return View(recipe_model);

		}


		[HttpPost]
		public IActionResult Create_message(RecipeDetailsView message_create)
		{
			int? recipe_id = HttpContext.Session.GetInt32("RecipeId");

			int? userid_int = HttpContext.Session.GetInt32("userId");


			var new_message = new MessageTable
			{
				RecipeId = (int)recipe_id,
				UserId = (int)userid_int,
				TopMessageid = message_create.FatherMessage,
				MessageContent = message_create.MessageContent,
				ExistDelete = "exist",
				CreateTime = DateTime.Now,
				ChangeTime = DateTime.Now
			};

			_context.MessageTables.Add(new_message);
			_context.SaveChanges();




			var view_model = GetRecipeDetailsView(recipe_id, userid_int);

			return PartialView("_message_craete", view_model);
		}

		[HttpPost]
		public IActionResult Delete_message(int id)
		{
			var message_delete = _context.MessageTables.Find(id);
			Console.WriteLine(message_delete);
			if (message_delete != null)
			{
				message_delete.ExistDelete = "delete";
				_context.SaveChanges();
			}


			int? recipe_id = HttpContext.Session.GetInt32("RecipeId");

			int? userid_int = HttpContext.Session.GetInt32("userId");


			var view_model = GetRecipeDetailsView(recipe_id, userid_int);


			return PartialView("_message_craete", view_model);
		}

		[HttpPost]
		public IActionResult Edit_message(int message_id, string message_content)
		{
			Console.WriteLine("message_id : " + message_id);
			Console.WriteLine("message_content : " + message_content);
			var message_edit = _context.MessageTables.Find(message_id);
			if (message_edit != null)
			{
				message_edit.ChangeTime = DateTime.Now;
				message_edit.MessageContent = message_content;
				_context.SaveChanges();
			}


			int? recipe_id = HttpContext.Session.GetInt32("RecipeId");

			int? userid_int = HttpContext.Session.GetInt32("userId");


			var view_model = GetRecipeDetailsView(recipe_id, userid_int);

			return PartialView("_message_craete", view_model);
		}


		private RecipeDetailsView GetRecipeDetailsView(int? recipeID, int? Userid)
		{
			int? recipe_id = recipeID;
			int? userid = Userid;
			var recipe = _context.RecipeTables
			 .Where(r => r.RecipeId == recipe_id)
			 .FirstOrDefault();

			var ingredient = _context.IngredientsTables
							 .Where(i => i.RecipeId == recipe_id)
							 .ToList();

			var step = _context.StepTables
					   .Where(s => s.RecipeId == recipe_id)
					   .ToList();

			var user = _context.UserInfos
					   .Where(u => u.UserId == recipe.UserId)
					   .FirstOrDefault();


			var message = _context.MessageTables
						  .Where(m => m.RecipeId == recipe_id)
						  .ToList();

			// 查詢與message關聯的users
			var message_userid = message.Select(m => m.UserId).Distinct().ToList();

			var users = _context.UserInfos
				.Where(u => message_userid.Contains(u.UserId))
				.ToList();

			// 建立用戶字典
			var user_dictionary = users.ToDictionary(u => u.UserId, u => u);

			// 建立一個包含所有留言和對應用戶信息的模型列表
			var message_with_user = message
									.Select(m => new
									{
										Message = m,
										User = user_dictionary.ContainsKey(m.UserId) ? user_dictionary[m.UserId] : null
									})
									.ToList();


			//查詢與message並將父message和子message分組排好
			var top_message = message_with_user
							  .Where(t => t.Message.TopMessageid == null)
							  .ToList();

			var child_message = message_with_user
							  .Where(t => t.Message.TopMessageid != null)
							  .ToList();

			var messages = top_message
						   .SelectMany(all_message => new[] { all_message }
						   .Concat(child_message.Where(child_message => child_message.Message.TopMessageid == all_message.Message.MessageId))).ToList();


			var login_userid = _context.UserInfos
					   .Where(u => u.UserId == userid)
					   .FirstOrDefault();



			return new RecipeDetailsView
			{
				Recipe = recipe,
				IngredientsTables = ingredient,
				StepTables = step,
				User = user,
				MessageTables = messages.Select(m => m.Message).ToList(),
				Message_users = messages.Select(m => m.User).ToList(),
				LoginUser = login_userid
			};

		}

	}
}
