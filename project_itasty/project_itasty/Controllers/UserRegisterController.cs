using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using project_itasty.Models;
using System.Security.Cryptography;
using BCrypt.Net;
using System.Net.Mail;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;



namespace project_itasty.Controllers
{

    public class UserRegisterController : Controller
    {
        private readonly ITastyDbContext _context;

        public UserRegisterController(ITastyDbContext context)
        {
            _context = context;
        }

        // GET: UserRegister
        public async Task<IActionResult> Index()
        {
            
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userEmail != null)
            {
               
                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail ==permission?.UserEmail&& permission.UserPermissions == 1)
                {
                   
                    return View(await _context.UserInfos.ToListAsync());
                }
                else
                {
                    
                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");


        }

        // GET: UserRegister/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userEmail != null)
            {

                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail == permission?.UserEmail && permission.UserPermissions == 1)
                {

                    if (id == null)
                    {
                        return NotFound();
                    }

                    var userInfo1 = await _context.UserInfos
                        .FirstOrDefaultAsync(m => m.UserId == id);
                    if (userInfo1 == null)
                    {
                        return NotFound();
                    }

                    return View(userInfo1);
                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");

           

           
        }

        // GET: UserRegister/Create
        public IActionResult Create()
        {
            TempData["createMessage"] = null;
            TempData["loginMessage"] = null;
            string userName = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userName != "Guest")
            {
                return Redirect("/Home/Index");
            }
            ViewData["UserId"] = new SelectList(_context.UserInfos, "Id", "Id");
            return View();
            
            
        }




        // POST: UserRegister/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("UserId,UserName,UserPhoto,UserEmail,UserPassword,UserBanner,UserIntro,UserPermissions")] UserInfo userInfo1,string UserPassword)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        string hashedPassword =  BCrypt.Net.BCrypt.HashPassword(UserPassword);
        //        userInfo1.UserPassword = hashedPassword;




        //        _context.UserInfos.Add(userInfo1);
        //        await _context.SaveChangesAsync();
        //        TempData["createMessage"] = "註冊成功";
        //        return View("Create"); //不會到這裡

        //    }

        //    return View();
        //}

        //TEST
        private static string generatedCode;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,UserPhoto,UserEmail,UserPassword,UserBanner,UserIntro,UserPermissions")] UserInfo userInfo1, string UserPassword,string UserEmail,string UserName)
        {

            if (ModelState.IsValid)
            {


                var emailbol = from o in _context.UserInfos where o.UserEmail == UserEmail select o;
                var emailbool = emailbol.FirstOrDefault();
                // 生成驗證碼
                Random random = new Random();
                generatedCode = random.Next(100000, 999999).ToString();
                if (emailbool!=null)
                {
                    return Json(new { success = false, error = "此信箱已註冊" });
                }
                   
                    // 發送電子郵件
                    SendVerificationEmail(UserEmail, generatedCode);

                    // 暫時保存用戶信息
                    TempData["Email"] = UserEmail;
                    TempData["Password"] = UserPassword;
                    TempData["UserName"] = UserName;
                    return Json(new { success = true });
                
               



            }

            return Json(new { success = false, error = "請輸入正確資料" });
        }
        [HttpGet]
        public ActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> VerifyEmail(string code ,UserInfo userInfo)
        {
            if (code == generatedCode)
            {
                // 驗證碼正確，完成註冊流程
                string email = TempData["Email"].ToString();
                string password = TempData["Password"].ToString();
                string UserName = TempData["UserName"].ToString();
                // 創建用戶（例如，保存到數據庫）
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                userInfo.UserPassword = hashedPassword;
                userInfo.UserEmail = email;
                userInfo.UserName = UserName;
                userInfo.UserPermissions = 2;
                _context.UserInfos.Add(userInfo);
                await _context.SaveChangesAsync();


                HttpContext.Session.SetString("userEmail", userInfo.UserEmail);
                HttpContext.Session.SetInt32("userId", userInfo.UserId);
                TempData["createMessage"] = "註冊成功";
                return Json(new { success = true });
            }
            ModelState.AddModelError("", "無效驗證碼");
            return Json(new { success = false, error = "無效驗證碼" });
        }

        private void SendVerificationEmail(string email, string code)
        {
            var fromAddress = new MailAddress("example@gmail.com", "Itasty");
            //"example@gmail.com"放email
            var toAddress = new MailAddress(email);
            const string fromPassword = "0000 0000 0000 0000";
            //fromPassword = "google應用程式密碼";
            const string subject = "ITASTY信箱驗證碼";
            string body = $"你的驗證碼是: {code}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        //TEST




        [HttpPost]

        public IActionResult Login(string UserEmail, string Userpassword)
        {
            //var user = _context.UserInfos1.SingleOrDefault(u => u.Email == userEmail);
            
            var query = from o in _context.UserInfos
                        where o.UserEmail == UserEmail 
                        select o;

            
            var member = query.FirstOrDefault();

            if (member == null)
            {
                //ViewData["message"]="帳號或密碼錯誤";
                TempData["loginMessage"] = "登入失敗";
                return RedirectToAction("Create", "UserRegister");
                
            }
            else
            {
               
                if (Userpassword == member.UserPassword)
                {
                    
					HttpContext.Session.SetString("userEmail", UserEmail);
					HttpContext.Session.SetInt32("userId", member.UserId);
					TempData["loginMessage"] = "登入成功";
                    return Redirect("/Home/Index");
                }
                else if (Userpassword == member.UserPassword|| BCrypt.Net.BCrypt.Verify(Userpassword, member.UserPassword) && member.UserPermissions == 1)
                {
                    HttpContext.Session.SetString("userEmail", UserEmail);
                    HttpContext.Session.SetInt32("userId", member.UserId);
                    HttpContext.Session.SetInt32("userPermissions", member.UserPermissions);
                    TempData["loginMessage"] = "管理員登入成功";
                    return Redirect("/Background_control/Index");
                }
                else if(BCrypt.Net.BCrypt.Verify(Userpassword, member.UserPassword)) 
                { 

                HttpContext.Session.SetString("userEmail", UserEmail);
                HttpContext.Session.SetInt32("userId", member.UserId);
                TempData["loginMessage"] = "登入成功";
                return Redirect("/Home/Index");
                }
                
                else
                {
                    TempData["loginMessage"] = "登入失敗";
                    return RedirectToAction("Create", "UserRegister");
                }
               
            }
          
           
        }

    		
	
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("userEmail");
            HttpContext.Session.Remove("userId");
            return Redirect("/Home/Index");
		}

        //TEST
        
        

   



        // GET: UserRegister/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userEmail != null)
            {

                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail == permission?.UserEmail && permission.UserPermissions == 1)
                {

                    if (id == null)
                    {
                        return NotFound();
                    }

                    var userInfo1 = await _context.UserInfos.FindAsync(id);
                    if (userInfo1 == null)
                    {
                        return NotFound();
                    }
                    return View(userInfo1);
                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");
           


            
        }

        // POST: UserRegister/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,UserPhoto,UserEmail,UserPassword,UserBanner,UserIntro,UserPermissions,UserCreateTime")] UserInfo userInfo1)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userEmail != null)
            {

                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail == permission?.UserEmail && permission.UserPermissions == 1)
                {
                    if (id != userInfo1.UserId)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(userInfo1);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!UserInfo1Exists(userInfo1.UserId))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    return View(userInfo1);
                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");


           

           
        }

        // GET: UserRegister/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userEmail != null)
            {

                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail == permission?.UserEmail && permission.UserPermissions == 1)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var userInfo1 = await _context.UserInfos
                        .FirstOrDefaultAsync(m => m.UserId == id);
                    if (userInfo1 == null)
                    {
                        return NotFound();
                    }

                    return View(userInfo1);

                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");


            

          
        }

        // POST: UserRegister/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userInfo1 = await _context.UserInfos.FindAsync(id);
            if (userInfo1 != null)
            {
                _context.UserInfos.Remove(userInfo1);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInfo1Exists(int id)
        {
            return _context.UserInfos.Any(e => e.UserId == id);
        }




        //
       


        //
    }
}
