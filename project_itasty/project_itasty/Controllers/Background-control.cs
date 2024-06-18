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


			//步驟1. 先將要導入的模型建好 用於顯示在view => (1)建好子模型 (2)在主模型註冊(要顯示在View上的model)

			//步驟2. 去View將要傳入API的資料準備好(使用ajax方法)
			//[需要的資料:reportId(對應的檢舉),isApproved(審核結果)]

			//步驟3. 撰寫 API [HTTP POST] 方法 (1)找出對應的檢舉(ReportTable)將ReportStatus改成true (2)找出檢舉的對象將審核結果紀錄在該張表內

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

			var RU_joinedList = from recipe in OrderrecipeList
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

			foreach (var item in RU_joinedList)
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
			var CommentList = _context.ReportTables.Where(r => r.ReportType == 1 && r.ReportStatus == false).ToList();//要改where的內容
			var MessageList = _context.MessageTables.ToList();

			var RecipedTableList = _context.RecipeTables.ToList();

			var CM_JoinedList = from comment in CommentList
								join message in MessageList on comment.RecipedIdOrCommentId equals message.MessageId
								join recipe in RecipedTableList on message.RecipeId equals recipe.RecipeId
								select new
								 {
								 ReportId= comment.ReportId,
								 RecipeId = recipe.RecipeId,
								 UserId = message.UserId,
								 MessageContent = message.MessageContent,
								 ReportReason = comment.ReportReason,
								 ReportStatus = comment.ReportStatus,
								 ReportUserId= comment.ReportUserId,
								};

			var CommentTable = new List<Background_Control_CommentTable>();

			foreach (var item in CM_JoinedList)
			{
				CommentTable.Add(new Background_Control_CommentTable
				{
					ReportId= item.ReportId,
					RecipeId = item.RecipeId,
					UserId = item.UserId,
					MessageContent = item.MessageContent,
					ReportReason = item.ReportReason,
					ReportStatus = item.ReportStatus,
					ReportUserId = item.ReportUserId,
				});
			}
			#endregion

			#region 將資料灌進 RecipeReport

			var RecipedReportList = _context.ReportTables.Where(r => r.ReportType == 2 && r.ReportStatus == false).ToList();
			var RecipeReport_userList=_context.UserInfos.ToList();
			var Reciped_Report_JoinedList = from RecipedReport in RecipedReportList
											join recipe in OrderrecipeList on RecipedReport.RecipedIdOrCommentId equals recipe.RecipeId
											join user in RecipeReport_userList on RecipedReport.ReportedUserId equals user.UserId
											select new
								{
									ReportId = RecipedReport.ReportId,
									RecipeId = recipe.RecipeId,
									RecipeName = recipe.RecipeName,
									Author = user.UserName,
									ReportReason = RecipedReport.ReportReason,
									ReportStatus = RecipedReport.ReportStatus,
								};




			var RecipeReport = new List<Background_Control_RecipeReport>();
			foreach (var item in Reciped_Report_JoinedList)
			{
				RecipeReport.Add(new Background_Control_RecipeReport
				{
					ReportId = item.ReportId,
					RecipeId = item.RecipeId,
					RecipeName = item.RecipeName,
					Author = item.Author,
					ReportReason = item.ReportReason,
					ReportStatus = item.ReportStatus,
				});
			}


			#endregion

			#region 將資料灌進 UserReport

			var UserReportList = _context.ReportTables.Where(r => r.ReportType == 0 && r.ReportStatus == false).ToList();
			var UserLists=_context.UserInfos.ToList();
			var User_Report_JoinedList = from userReport in UserReportList
										 join user in UserLists on userReport.ReportedUserId equals user.UserId
											select new
											{
												ReportId = userReport.ReportId,
												UserId = userReport.ReportedUserId,
												UserName = user.UserName,
												Email = user.UserEmail,
												ReportReason = userReport.ReportReason,
												ReportStatus = userReport.ReportStatus,
											};

			var UserReport = new List<Background_Control_UserReport>();
			foreach (var item in User_Report_JoinedList)
			{
				UserReport.Add(new Background_Control_UserReport
				{
					ReportId = item.ReportId,
					UserId = item.UserId,
					UserName = item.UserName,
					Email = item.Email,
					ReportReason = item.ReportReason,
					ReportStatus = item.ReportStatus,
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
				CommentTable= CommentTable,
				RecipeReport= RecipeReport,
				UserReport= UserReport,
			};


			ViewBag.Month = DateTime.Now.Month; ;
			var query = from o in _context.SeasonalIngredients
						where o.MonthId == 7 /*month*/
						select o;
			ViewBag.season = query;

			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> ReviewComment(int reportId, bool isApproved)
		{
			try
			{
				// 查找對應的檢舉
				var report = await _context.ReportTables.FirstOrDefaultAsync(r => r.ReportId == reportId);
				if (report != null)
				{
					// 更新檢舉狀態
					report.ReportStatus = true;

					// 查找對應的留言
					var message = await _context.MessageTables.FirstOrDefaultAsync(m => m.MessageId == report.RecipedIdOrCommentId);
					if (message != null)
					{
						// 更新留言的violationStatus字段
						message.ViolationStatus = isApproved ? "violation" : "No violation";
					}

					// 保存更改
					_context.Update(report);
					if (message != null)
					{
						_context.Update(message);
					}
					await _context.SaveChangesAsync();

					return Json(new { success = true });
				}
				return Json(new { success = false, message = "找不到對應的檢舉" });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public async Task<IActionResult> ReviewUser(int reportId, bool isApproved)
		{
			try
			{
				// 查找對應的檢舉
				var report = await _context.ReportTables.FirstOrDefaultAsync(r => r.ReportId == reportId);
				if (report != null)
				{
					// 更新檢舉狀態
					report.ReportStatus = true;

					// 查找對應的留言
					var user = await _context.UserInfos.FirstOrDefaultAsync(u => u.UserId == report.RecipedIdOrCommentId);
					if (user != null)
					{
						// 更新留言的violationStatus字段
						user.UserPermissions = isApproved ? 3 : 2;
					}

					// 保存更改
					_context.Update(report);
					if (user != null)
					{
						_context.Update(user);
					}
					await _context.SaveChangesAsync();

					return Json(new { success = true });
				}
				return Json(new { success = false, message = "找不到對應的檢舉" });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}
		[HttpPost]
		public async Task<IActionResult> ReviewRecipe(int reportId, bool isApproved)
		{
			try
			{
				// 查找對應的檢舉
				var report = await _context.ReportTables.FirstOrDefaultAsync(r => r.ReportId == reportId);
				if (report != null)
				{
					// 更新檢舉狀態
					report.ReportStatus = true;

					// 查找對應的留言
					var recipe = await _context.RecipeTables.FirstOrDefaultAsync(u => u.UserId == report.RecipedIdOrCommentId);
					if (recipe != null)
					{
						// 更新留言的violationStatus字段
						recipe.RecipeStatus = isApproved ? "violation" : "No violation";
					}

					// 保存更改
					_context.Update(report);
					if (recipe != null)
					{
						_context.Update(recipe);
					}
					await _context.SaveChangesAsync();

					return Json(new { success = true });
				}
				return Json(new { success = false, message = "找不到對應的檢舉" });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}


	}
}
