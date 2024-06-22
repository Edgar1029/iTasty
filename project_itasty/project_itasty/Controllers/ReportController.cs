using Microsoft.AspNetCore.Mvc;
using project_itasty.Models;
using System.Linq;

namespace project_itasty.Controllers
{
	public class ReportController : Controller
	{
		private readonly ITastyDbContext _context;
		public ReportController(ITastyDbContext context)
		{
			_context = context;
		}


         public IActionResult Index(int ID, int Type,int reportUserID)
         {
            //加入Type判斷，如果Type傳入 0 則使用 UserInfo 來判斷 ID ，Type傳入 1 則使用 MessageTable 來判斷 ID ，Type傳入 2 則使用 RecipeTable來判斷 ID 。

            //連接iTastyDB 將傳入的資料寫入ReportTable，其中ID 對接 ReportTable 裡的 RecipedIdOrCommentId ， Type 對接 ReportTable 裡的ReportType

            //我還需要將 UserID 轉換成 UserName 植入 Report頁面 => 需要使用 LinQ ， 將 id 都 join 起來

            //假裝傳入
            //ID = 3;
            //Type = 1;
            //reportUserID = 2;

         string userName = string.Empty;
         string notes = string.Empty;
        string reportUserName = string.Empty;

         var UserInfo = _context.UserInfos.FirstOrDefault(u => u.UserId == reportUserID);
         if (UserInfo != null)
         {
             reportUserName = UserInfo.UserName;
         }

            switch (Type)
         {
             case 0:
                 var userInfo = _context.UserInfos.FirstOrDefault(u => u.UserId == ID);
                 if (userInfo != null)
                 {
                     userName = userInfo.UserName;
                     notes = userInfo.UserName;
                 }
                 break;
             case 1:
                 var message = (from m in _context.MessageTables
                                join u in _context.UserInfos on m.UserId equals u.UserId
                                where m.MessageId == ID
                                select new { m, u.UserName }).FirstOrDefault();
                 if (message != null)
                 {
                     userName = message.UserName;
                     notes =  message.m.MessageContent; 
                 }
                 break;
             case 2:
                 var recipe = (from r in _context.RecipeTables
                               join u in _context.UserInfos on r.UserId equals u.UserId
                               where r.RecipeId == ID
                               select new { r, u.UserName }).FirstOrDefault();
                 if (recipe != null)
                 {
                     userName = recipe.UserName;
                     notes = recipe.r.RecipeName; 
                 }
                 break;
             default:
                 break;
         }

         ViewBag.UserName = userName;
         ViewBag.Notes = notes;
         ViewBag.ID = ID;
         ViewBag.Type = Type;
         ViewBag.reportUserName = reportUserName;
         ViewBag.reportUserID = reportUserID;

         return View();
         }
    }
}
