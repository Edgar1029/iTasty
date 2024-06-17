using Microsoft.AspNetCore.Mvc;
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

		[HttpPost("recipe")]
		public ActionResult Index()
		{
            int recipe_id = int.Parse(Request.Form["recipe_id"]);

         
           
			


			var recipe = _context.RecipeTables
						 .Where(r => r.RecipeId == recipe_id)
						 .FirstOrDefault();

			var ingredient = _context.IngredientsTables
							 .Where(r => r.RecipeId == recipe_id)
							 .ToList();

			var step = _context.StepTables
					   .Where(r => r.RecipeId == recipe_id)
					   .ToList();

			var user = _context.UserInfos
					   .Where(r => r.UserId == recipe.UserId)
					   .FirstOrDefault();

			var message = _context.MessageTables
						  .Where(r => r.RecipeId == recipe_id)
						  .ToList();



			var recipe_model = new RecipeDetailsView
			{
				Recipe = recipe,
				IngredientsTables = ingredient,
				StepTables = step,
				User = user,
				MessageTables = message,
			};

			return View(recipe_model);
		}


		[HttpPost]
		public ActionResult Index(MessageTable messages, RecipeDetailsView recipe_model)
		{

			int userid = 96;

			var father_id = (from message in _context.MessageTables
							where messages.ParentTime == message.CreateTime &&
								  messages.ParentMessage == message.MessageContent &&
								  messages.ParentUserId == message.UserId
							select message.MessageId).FirstOrDefault();

			messages.TopMessageid = father_id;
			messages.UserId = userid;
            //messages.RecipeId = recipe_model.Recipe.RecipeId;
            messages.RecipeId = 1;
            messages.CreateTime = DateTime.Now;
			messages.ChangeTime = DateTime.Now;

			_context.MessageTables.Add(messages);
			//_context.SaveChanges();



			return View(recipe_model);
		}


	}
}
