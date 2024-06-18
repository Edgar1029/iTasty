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

			int userid = HttpContext.Session?.GetInt32("userId")??0;
			

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



			var recipe_model = new RecipeDetailsView
			{
				Recipe = recipe,
				IngredientsTables = ingredient,
				StepTables = step,
				User = user,
				MessageTables = messages.Select(m => m.Message).ToList(),
				Message_users = messages.Select(m => m.User).ToList(),
				LoginUser = login_userid
			};


			Console.WriteLine(recipe_model.MessageTables.Count);
			Console.WriteLine(recipe_model.Message_users.Count);





			return View(recipe_model);
		}


		public IActionResult Create_message(RecipeDetailsView message_create)
		{
			
				int recipe_id = 1;

				int userid = 33;


				var new_message = new MessageTable
				{
					RecipeId = recipe_id,
					UserId = userid,
					TopMessageid = message_create.FatherMessage,
					MessageContent = message_create.MessageContent,
					CreateTime = DateTime.Now,
					ChangeTime = DateTime.Now
				};
			Console.WriteLine("TopMessageid:" + message_create.FatherMessage);
			Console.WriteLine("MessageContent" + message_create.MessageContent);

			_context.MessageTables.Add(new_message);
			_context.SaveChanges();

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

				var view_model = new RecipeDetailsView
				{
					Recipe = recipe,
					IngredientsTables = ingredient,
					StepTables = step,
					User = user,
					MessageTables = messages.Select(m => m.Message).ToList(),
					Message_users = messages.Select(m => m.User).ToList(),
					LoginUser = login_userid
				};
				return PartialView("_message_craete", view_model);
			
		}

	}
}
