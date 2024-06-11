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
								  where obj.UserEmail.IndexOf(email + '@') == 0
								  select obj;

			UserInfo? user_info = select_userinfo.FirstOrDefault();

			if (user_info == null)
			{
				return Redirect("~/UserRegister/Create");
			}

			var query = from o in _context.UserFollowers
						where o.UserId == user_info.UserId & o.UnfollowDate == null
						join u in _context.UserInfos on o.FollowerId equals u.UserId
						orderby o.FollowDate descending
						select new { o, u };

			ViewBag.user = query;
			ViewBag.user_count = query.Count();
			foreach (var item in query)
			{
				var follow_count = from c in _context.UserFollowers
								   where c.UserId == item.u.UserId & c.UnfollowDate == null
								   select c;
				ViewData[$"follower_{item.u.UserId}"] = follow_count;

				var recipe_count = from c in _context.RecipeTables
								   where c.UserId == item.u.UserId
								   select c;
				ViewData[$"recipe_{item.u.UserId}"] = recipe_count.Count();

			}

			var query_recipe = from r in _context.RecipeTables
							   where r.UserId == user_info.UserId
							   select r;
			ViewBag.recipe = query_recipe;
			ViewBag.recipe_count = query_recipe.Count();
			foreach( var item in query_recipe)
			{
				var recipe_view_count = from v in _context.RecipeViews
								   where v.RecipeId == item.RecipeId
								   select v;
				ViewData[$"recipe_view_{item.RecipeId}"] = recipe_view_count.Sum(x => x.ViewNum);
			}


			return View(user_info);
		}
	}
}
