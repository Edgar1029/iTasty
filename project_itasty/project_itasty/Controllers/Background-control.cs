using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project_itasty.Models;
using System.Linq;

namespace project_itasty.Controllers
{
	public class Background_control : Controller
	{
		private readonly ITastyDbContext _context;
		public Background_control(ITastyDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> index()
		{
			#region Chart 圖表
			var chartData1 = new Background_Control_ChartData
			{
				Labels = await _context.RecipeTables.Select(r => r.RecipeName).ToListAsync(),
				Data = await _context.RecipeTables.Select(r => r.Views).ToListAsync()
			};

			var chartData2 = new Background_Control_ChartData
			{
				Labels = new List<string> { "January", "February", "March", "April", "May", "June" },
				Data = new List<int> { 12, 19, 3, 5, 2, 3 }
			};
			#endregion

			#region 從recipedTable拿資料，並把資料灌進自建的模型(Background_Control_OrderRecipedTable)

			var OrderrecipeList = _context.RecipeTables.OrderByDescending(r => r.Views).ToList();
			var userList = _context.UserInfos.ToList();

			var joinedList = from recipe in OrderrecipeList
							 join user in userList on recipe.UserId equals user.UserId
							 select new
							 {
								 RecipedName = recipe.RecipeName,
								 Author = user.UserName,
								 RecipedView = recipe.Views,
								 NumberOfComment = recipe.Favorites,
								 RecipedStatus = recipe.RecipeStatus
							 };

			var OrderRecipedTable = new List<Background_Control_OrderRecipedTable>();

			foreach (var item in joinedList)
			{
				OrderRecipedTable.Add(new Background_Control_OrderRecipedTable
				{
					RecipedName = item.RecipedName,
					Author = item.Author,
					RecipedView = item.RecipedView,
					NumberOfComment = item.NumberOfComment,
					RecipedStatus = item.RecipedStatus
				});
			}

			#endregion

			#region 將資料灌進UserList
			var UserList = _context.UserInfos.Where(r=>r.UserPermissions == 1 || r.UserPermissions ==3).OrderBy(r=>r.UserPermissions).ToList();

			var UserTable = new List<Background_Control_UserTable>();

			foreach (var item in UserList)
			{
				UserTable.Add(new Background_Control_UserTable
				{
					UserId = item.UserId,
					UserName = item.UserName,
					Email = item.UserEmail,
					UserStatus = item.UserPermissions,
				});
			}
			#endregion

			#region 將資料灌進RecipedTable
			var recipeList = _context.RecipeTables.ToList();

			var JoinedList = from recipe in recipeList
							 join user in userList on recipe.UserId equals user.UserId
							 select new
							 {
								 RecipedId = recipe.RecipeId,
								 RecipedName = recipe.RecipeName,
								 Author = user.UserName,
								 RecipedView = recipe.Views,
								 NumberOfComment = recipe.Favorites,
								 RecipedStatus = recipe.RecipeStatus
							 };

			var RecipedTable = new List<Background_Control_RecipedTable>();

			foreach (var item in JoinedList)
			{
				RecipedTable.Add(new Background_Control_RecipedTable
				{
					RecipedId = item.RecipedId,
					RecipedName = item.RecipedName,
					Author = item.Author,
					RecipedView = item.RecipedView,
					NumberOfComment = item.NumberOfComment,
					RecipedStatus = item.RecipedStatus
				});
			}

			#endregion

			#region 將資料灌進 Comment
			var CommentList = _context.MessageTables.Where(r => r.ViolationStatus == "Violation Reported").ToList();

			var CommentTable = new List<Background_Control_CommentTable>();

			foreach (var item in CommentList)
			{
				CommentTable.Add(new Background_Control_CommentTable
				{
					RecipeId = item.RecipeId,
					UserId = item.UserId,
					MessageContent = item.MessageContent,
					ViolationStatus = item.ViolationStatus,
				});
			}
			#endregion

			var model = new Backgroud_Control_Model
			{
				ChartViewsData = chartData1,
				ChartMembershipData = chartData2,
				OrderRecipedTable = OrderRecipedTable,
				UserTable = UserTable,
				RecipedTable = RecipedTable,
			};


			ViewBag.Month = DateTime.Now.Month; ;
			var query = from o in _context.SeasonalIngredients
						where o.MonthId == 7 /*month*/
						select o;
			ViewBag.season = query;

			return View(model);

		}
	}
}
