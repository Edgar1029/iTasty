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
		[HttpGet("{id}")]
		public IActionResult Index(int id)
		{
			var userinfo = _context.UserInfos.Find(id);

			var query = from o in _context.UserFollowers
						   where o.UserId == id
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
            return View(userinfo);
		}
	}
}
