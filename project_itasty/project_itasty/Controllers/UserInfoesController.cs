using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_itasty.Models;

namespace project_itasty.Controllers
{
    public class UserInfoesController : Controller
    {
        private readonly ITastyDbContext _context;

        public UserInfoesController(ITastyDbContext context)
        {
            _context = context;
        }

        // GET: UserInfoes
        [HttpGet("user/{email}")]
        public IActionResult Index(string email)
        {
            //var userinfo = _context.UserInfos.Find(id);
            var select_userinfo = from obj in _context.UserInfos
                                  where obj.Email.IndexOf(email+'@') == 0
                                  select obj;

            UserInfo user_info = select_userinfo.First();

            var query = from o in _context.UserFollowers
                        where o.UserId == user_info.UserId
                        join u in _context.UserInfos on o.FollowerId equals u.UserId
                        orderby o.FollowDate descending
                        select new { o, u };

            ViewBag.user = query;
            foreach (var item in query)
            {
                var count = from c in _context.UserFollowers
                            where c.UserId == item.u.UserId
                            select c;
                ViewData[$"follower_{item.u.UserId}"] = count;
            }
            return View(user_info);
        }
    }
}
