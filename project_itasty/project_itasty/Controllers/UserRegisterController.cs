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
            string userName = HttpContext.Session.GetString("userEmail") ?? "Guest";
            var query=from o in _context.UserInfos where o.UserPermissions==1 select o;
            var permission=query.ToList();
            foreach(var i in permission)
            {
                if (i.UserPermissions == 1&&i.UserEmail==userName&& userName!="Guest")
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
            string userName = HttpContext.Session.GetString("userEmail") ?? "Guest";
            var query = from o in _context.UserInfos where o.UserPermissions == 1 select o;
            var permission = query.ToList();
            foreach (var i in permission)
            {
                if (i.UserPermissions == 1 && i.UserEmail == userName && userName != "Guest")
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,UserPhoto,UserEmail,UserPassword,UserBanner,UserIntro,UserPermissions")] UserInfo userInfo1,string UserPassword)
        {
           
            if (ModelState.IsValid)
            {
                string hashedPassword =  BCrypt.Net.BCrypt.HashPassword(UserPassword);
                userInfo1.UserPassword = hashedPassword;
                
                
                
                
                _context.UserInfos.Add(userInfo1);
                await _context.SaveChangesAsync();
                TempData["createMessage"] = "註冊成功";
                return View("Create"); //不會到這裡
               
            }

            return View();
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
                TempData["loginMessage"] = "登入失敗";
                 if (Userpassword == member.UserPassword)
                {
					HttpContext.Session.SetString("userEmail", UserEmail);
					HttpContext.Session.SetInt32("userId", member.UserId);
					TempData["loginMessage"] = "登入成功";
                    return Redirect("/Home/Index");
                }
                else 
                if(BCrypt.Net.BCrypt.Verify(Userpassword, member.UserPassword)) 
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
            HttpContext.Session.Remove("userid");
            return Redirect("/Home/Index");
		}

        //TEST
        
        

   



        // GET: UserRegister/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string userName = HttpContext.Session.GetString("userEmail") ?? "Guest";
            var query = from o in _context.UserInfos where o.UserPermissions == 1 select o;
            var permission = query.ToList();
            foreach (var i in permission)
            {
                if (i.UserPermissions == 1 && i.UserEmail == userName && userName != "Guest")
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

        // GET: UserRegister/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string userName = HttpContext.Session.GetString("userEmail") ?? "Guest";
            var query = from o in _context.UserInfos where o.UserPermissions == 1 select o;
            var permission = query.ToList();
            foreach (var i in permission)
            {
                if (i.UserPermissions == 1 && i.UserEmail == userName && userName != "Guest")
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
