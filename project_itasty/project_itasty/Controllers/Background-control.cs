using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project_itasty.Models;
using System.Composition;
using System.Linq;

//因效率緩慢所以使用異步方法
// 修改前

//namespace project_itasty.Controllers
//{
//	public class Background_control : Controller
//	{
//		private readonly ITastyDbContext _context;
//		public Background_control(ITastyDbContext context)
//		{
//			_context = context;
//		}
//		public async Task<IActionResult> index()
//		{


//			//步驟1. 先將要導入的模型建好 用於顯示在view => (1)建好子模型 (2)在主模型註冊(要顯示在View上的model)

//			//步驟2. 去View將要傳入API的資料準備好(使用ajax方法)
//			//[需要的資料:reportId(對應的檢舉),isApproved(審核結果)]

//			//步驟3. 撰寫 API [HTTP POST] 方法 (1)找出對應的檢舉(ReportTable)將ReportStatus改成true (2)找出檢舉的對象將審核結果紀錄在該張表內

//			#region Chart 圖表
//			var chartData1 = new Background_Control_ChartData
//			{
//				Labels = await _context.RecipeTables.Select(r => r.RecipeName).ToListAsync(),
//				Data = await _context.RecipeTables.Select(r => r.Views).ToListAsync()
//			};

//			var chartData2 = new Background_Control_ChartData
//			{
//				Labels = new List<string> { "January", "February", "March", "April", "May", "June" },
//				Data = new List<int> { 12, 19, 3, 5, 2, 3 }
//			};
//			#endregion

//			#region 從recipedTable拿資料，並把資料灌進自建的模型(Background_Control_OrderRecipedTable)

//			var OrderrecipeList = _context.RecipeTables.OrderByDescending(r => r.Views);

//			var RU_joinedList = from recipe in OrderrecipeList
//							 //join user in userList on recipe.UserId equals user.UserId
//							 select new
//							 {
//								 RecipedName = recipe.RecipeName,
//								 Author =recipe.User.UserName,
//								 RecipedView = recipe.Views,
//								 NumberOfComment = recipe.Favorites,
//								 RecipedStatus = recipe.RecipeStatus
//							 };

//			var OrderRecipedTable = new List<Background_Control_OrderRecipedTable>();

//			foreach (var item in RU_joinedList)
//			{
//				OrderRecipedTable.Add(new Background_Control_OrderRecipedTable
//				{
//					RecipedName = item.RecipedName,
//					Author = item.Author,
//					RecipedView = item.RecipedView,
//					NumberOfComment = item.NumberOfComment,
//					RecipedStatus = item.RecipedStatus
//				});
//			}

//			#endregion

//			#region 將資料灌進UserList
//			// 用戶列表
//			var users = await _context.UserInfos
//				.Where(u => u.UserPermissions == 1 || u.UserPermissions == 3)
//				.OrderBy(u => u.UserPermissions)
//				.Select(u => new Background_Control_UserTable
//				{
//					UserId = u.UserId,
//					UserName = u.UserName,
//					Email = u.UserEmail,
//					UserStatus = u.UserPermissions
//				}).ToListAsync();
//			#endregion

//			#region 將資料灌進RecipedTable
//			var JoinedList =  from recipe in _context.RecipeTables
//								  //join user in userList on recipe.UserId equals user.UserId
//							  select new
//							 {
//								 RecipedId = recipe.RecipeId,
//								 RecipedName = recipe.RecipeName,
//								 Author = recipe.User.UserName,
//								 RecipedView = recipe.Views,
//								 NumberOfComment = recipe.Favorites,
//								 RecipedStatus = recipe.RecipeStatus
//							 };

//			var RecipedTable = new List<Background_Control_RecipedTable>();

//			foreach (var item in JoinedList)
//			{
//				RecipedTable.Add(new Background_Control_RecipedTable
//				{
//					RecipedId = item.RecipedId,
//					RecipedName = item.RecipedName,
//					Author = item.Author,
//					RecipedView = item.RecipedView,
//					NumberOfComment = item.NumberOfComment,
//					RecipedStatus = item.RecipedStatus
//				});
//			}

//			#endregion

//			#region 將資料灌進 Comment
//			var CommentList = _context.ReportTables.Where(r => r.ReportType == 1 && r.ReportStatus == false);//要改where的內容
//			var MessageList = _context.MessageTables;

//			var RecipedTableList = _context.RecipeTables;

//			var CM_JoinedList = from comment in CommentList
//								join message in MessageList on comment.RecipedIdOrCommentId equals message.MessageId
//								//join recipe in RecipedTableList on message.RecipeId equals recipe.RecipeId
//								select new
//								 {
//								 ReportId= comment.ReportId,
//								 RecipeId = message.Recipe.RecipeId,
//								 UserId = message.UserId,
//								 MessageContent = message.MessageContent,
//								 ReportReason = comment.ReportReason,
//								 ReportStatus = comment.ReportStatus,
//								 ReportUserId= comment.ReportUserId,
//								};

//			var CommentTable = new List<Background_Control_CommentTable>();

//			foreach (var item in CM_JoinedList)
//			{
//				CommentTable.Add(new Background_Control_CommentTable
//				{
//					ReportId= item.ReportId,
//					RecipeId = item.RecipeId,
//					UserId = item.UserId,
//					MessageContent = item.MessageContent,
//					ReportReason = item.ReportReason,
//					ReportStatus = item.ReportStatus,
//					ReportUserId = item.ReportUserId,
//				});
//			}
//			#endregion

//			#region 將資料灌進 RecipeReport

//			var RecipedReportList = _context.ReportTables.Where(r => r.ReportType == 2 && r.ReportStatus == false).ToList();
//			var RecipeReport_userList=_context.UserInfos.ToList();
//			var Reciped_Report_JoinedList = from RecipedReport in RecipedReportList
//											join recipe in OrderrecipeList on RecipedReport.RecipedIdOrCommentId equals recipe.RecipeId
//											//join user in RecipeReport_userList on RecipedReport.ReportedUserId equals user.UserId
//											select new
//								{
//									ReportId = RecipedReport.ReportId,
//									RecipeId = recipe.RecipeId,
//									RecipeName = recipe.RecipeName,
//									Author = RecipedReport.ReportedUser.UserName,
//									ReportReason = RecipedReport.ReportReason,
//									ReportStatus = RecipedReport.ReportStatus,
//									Reporter = RecipedReport.ReportUserId,
//											};




//			var RecipeReport = new List<Background_Control_RecipeReport>();
//			foreach (var item in Reciped_Report_JoinedList)
//			{
//				RecipeReport.Add(new Background_Control_RecipeReport
//				{
//					ReportId = item.ReportId,
//					RecipeId = item.RecipeId,
//					RecipeName = item.RecipeName,
//					Author = item.Author,
//					ReportReason = item.ReportReason,
//					ReportStatus = item.ReportStatus,
//					Reporter = item.Reporter,
//				});
//			}


//			#endregion

//			#region 將資料灌進 UserReport

//			var UserReportList = _context.ReportTables.Where(r => r.ReportType == 0 && r.ReportStatus == false).ToList();
//			var UserLists=_context.UserInfos.ToList();
//			var User_Report_JoinedList = from userReport in UserReportList
//										 //join user in UserLists on userReport.ReportedUserId equals user.UserId
//											select new
//											{
//												ReportId = userReport.ReportId,
//												UserId = userReport.ReportedUserId,
//												UserName = userReport.ReportedUser.UserName,
//												Email = userReport.ReportedUser.UserEmail,
//												ReportReason = userReport.ReportReason,
//												ReportStatus = userReport.ReportStatus,
//											};

//			var UserReport = new List<Background_Control_UserReport>();
//			foreach (var item in User_Report_JoinedList)
//			{
//				UserReport.Add(new Background_Control_UserReport
//				{
//					ReportId = item.ReportId,
//					UserId = item.UserId,
//					UserName = item.UserName,
//					Email = item.Email,
//					ReportReason = item.ReportReason,
//					ReportStatus = item.ReportStatus,
//				});
//			}


//			#endregion

//			var model = new Backgroud_Control_Model
//			{
//				ChartViewsData = chartData1,
//				ChartMembershipData = chartData2,
//				OrderRecipedTable = OrderRecipedTable,
//				UserTable = users,
//				RecipedTable = RecipedTable,
//				CommentTable= CommentTable,
//				RecipeReport= RecipeReport,
//				UserReport= UserReport,
//			};


//			ViewBag.Month = DateTime.Now.Month; ;
//			var query = from o in _context.SeasonalIngredients
//						where o.MonthId == 7 /*month*/
//						select o;
//			ViewBag.season = query;

//			return View(model);

//		}



//修改後

namespace project_itasty.Controllers
{
	public class Background_control : Controller
	{
		private readonly ITastyDbContext _context;

		public Background_control(ITastyDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            int userPermissionsl = HttpContext.Session.GetInt32("userPermissions") ?? 2;
            if (userEmail != null)
            {

                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail == permission?.UserEmail && permission.UserPermissions == 1)
                {

                    // 圖表數據
                    var chartData1 = new Background_Control_ChartData
                    {
                        Labels = await _context.RecipeTables.Select(r => r.RecipeName).ToListAsync(),
                        Data = await _context.RecipeTables.Select(r => r.Views).ToListAsync()
                    };

                    var chartData2 = new Background_Control_ChartData
                    {
                        Labels = new List<string> { "一月", "二月", "三月", "四月", "五月", "六月" },
                        Data = new List<int> { 125, 198, 35, 509, 256, 378 }
                    };

                    // 排序的菜譜列表
                    var orderedRecipes = await _context.RecipeTables
                        .OrderByDescending(r => r.Views)
                        .Select(r => new Background_Control_OrderRecipedTable
                        {
                            RecipedName = r.RecipeName,
                            Author = r.User.UserName,
                            RecipedView = r.Views,
                            NumberOfComment = r.Favorites,
                            RecipedStatus = r.RecipeStatus
                        }).ToListAsync();

                    // 用戶列表
                    var users = await _context.UserInfos
                        .Where(u => u.UserPermissions == 1 || u.UserPermissions == 3)
                        .OrderBy(u => u.UserPermissions)
                        .Select(u => new Background_Control_UserTable
                        {
                            UserId = u.UserId,
                            UserName = u.UserName,
                            Email = u.UserEmail,
                            UserStatus = u.UserPermissions
                        }).ToListAsync();

                    // 所有菜譜
                    var recipes = await _context.RecipeTables
                        .Select(r => new Background_Control_RecipedTable
                        {
                            RecipedId = r.RecipeId,
                            RecipedName = r.RecipeName,
                            Author = r.User.UserName,
                            RecipedView = r.Views,
                            NumberOfComment = r.Favorites,
                            RecipedStatus = r.RecipeStatus
                        }).ToListAsync();

                    // 評論檢舉
                    var comments = await (from comment in _context.ReportTables
                                          where comment.ReportType == 1 && comment.ReportStatus == false
                                          join message in _context.MessageTables on comment.RecipedIdOrCommentId equals message.MessageId
                                          select new Background_Control_CommentTable
                                          {
                                              ReportId = comment.ReportId,
                                              RecipeId = message.Recipe.RecipeId,
                                              UserId = message.UserId,
                                              MessageContent = message.MessageContent,
                                              ReportReason = comment.ReportReason,
                                              ReportStatus = comment.ReportStatus,
                                              ReportUserId = comment.ReportUserId
                                          }).ToListAsync();

                    // 菜譜檢舉
                    var recipeReports = await (from report in _context.ReportTables
                                               where report.ReportType == 2 && report.ReportStatus == false
                                               join recipe in _context.RecipeTables on report.RecipedIdOrCommentId equals recipe.RecipeId
                                               join user in _context.UserInfos on report.ReportUserId equals user.UserId
                                               select new Background_Control_RecipeReport
                                               {
                                                   ReportId = report.ReportId,
                                                   RecipeId = recipe.RecipeId,
                                                   RecipeName = recipe.RecipeName,
                                                   Author = report.ReportedUser.UserName,
                                                   ReportReason = report.ReportReason,
                                                   ReportStatus = report.ReportStatus,
                                                   Reporter = user.UserName
                                               }).ToListAsync();

                    // 用戶檢舉
                    var userReports = await (from report in _context.ReportTables
                                             where report.ReportType == 0 && report.ReportStatus == false
                                             join user in _context.UserInfos on report.ReportUserId equals user.UserId
                                             select new Background_Control_UserReport
                                             {
                                                 ReportId = report.ReportId,
                                                 UserId = report.ReportedUserId,
                                                 UserName = report.ReportedUser.UserName,
                                                 Email = report.ReportedUser.UserEmail,
                                                 ReportReason = report.ReportReason,
                                                 ReportStatus = report.ReportStatus,
                                                 ReportUser = user.UserName,
                                             }).ToListAsync();

                    //問題表單
                    var issueReports = await (from r in _context.HelpForms
                                              select new Background_Control_IssueReport
											  { 
                                                  FormId = r.FormId,
                                                  UserId=r.UserId,
                                                  QuestionContent = r.QuestionContent,
												  QuestionType = r.QuestionType,
                                                  QuestionImage = r.QuestionImage,    
                                              }).ToListAsync();

                    var model = new Backgroud_Control_Model
                    {
                        ChartViewsData = chartData1,
                        ChartMembershipData = chartData2,
                        OrderRecipedTable = orderedRecipes,
                        UserTable = users,
                        RecipedTable = recipes,
                        CommentTable = comments,
                        RecipeReport = recipeReports,
                        UserReport = userReports,
                        IssueReport = issueReports,
                    };

                    ViewBag.Month = DateTime.Now.Month;
                    ViewBag.Season = await _context.SeasonalIngredients
                        .Where(o => o.MonthId == 7)
                        .ToListAsync();

                    return View(model);


                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");

          
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
