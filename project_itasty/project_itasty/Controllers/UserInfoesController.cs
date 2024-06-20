using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		//透過LINQ搜尋
		//[HttpGet("user/{email}")]
		//public IActionResult Index(string email)
		//{
		//	Stopwatch stopwatch = Stopwatch.StartNew();
		//	//var userinfo = _context.UserInfos.Find(id);
		//	var select_userinfo = from obj in _context.UserInfos
		//						  where obj.UserEmail.IndexOf(email + '@') == 0
		//						  select obj;

		//	UserInfo? user_info = select_userinfo.FirstOrDefault();

		//	if (user_info == null)
		//	{
		//		return Redirect("~/UserRegister/Create");
		//	}

		//	var query = from o in _context.UserFollowers
		//				where o.UserId == user_info.UserId & o.UnfollowDate == null
		//				join u in _context.UserInfos on o.FollowerId equals u.UserId
		//				orderby o.FollowDate descending
		//				select new { o, u };

		//	ViewBag.user = query;
		//	ViewBag.user_count = query.Count();
		//	foreach (var item in query)
		//	{
		//		var follow_count = from c in _context.UserFollowers
		//						   where c.UserId == item.u.UserId & c.UnfollowDate == null
		//						   select c;
		//		ViewData[$"follower_{item.u.UserId}"] = follow_count;

		//		var recipe_count = from c in _context.RecipeTables
		//						   where c.UserId == item.u.UserId
		//						   select c;
		//		ViewData[$"recipe_{item.u.UserId}"] = recipe_count.Count();

		//	}

		//	var query_recipe = from r in _context.RecipeTables
		//					   where r.UserId == user_info.UserId
		//					   select r;
		//	ViewBag.recipe = query_recipe;
		//	ViewBag.recipe_count = query_recipe.Count();
		//	foreach (var item in query_recipe)
		//	{
		//		var recipe_view_count = from v in _context.RecipeViews
		//								where v.RecipeId == item.RecipeId
		//								select v;
		//		ViewData[$"recipe_view_{item.RecipeId}"] = recipe_view_count.Sum(x => x.ViewNum);
		//	}

		//	stopwatch.Stop();
		//	Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
		//	Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
		//	Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
		//	return View(user_info);
		//}

		//透過Dictionary搜尋
		//GET: UserInfoes
	   [HttpGet("user/{email}")]
		public IActionResult Index(string email)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();

			var dict_userinfo = _context.UserInfos
				.ToDictionary(o => o.UserEmail.Split('@')[0]);

			UserInfo? user_info = dict_userinfo[email];

			if (user_info == null)
			{
				return Redirect("~/");
			}

			// 将 UserFollowers 转换为 Dictionary
			var userFollowersDict = _context.UserFollowers
				.Where(o => o.UserId == user_info.UserId && o.UnfollowDate == null)
				.OrderByDescending(o => o.FollowDate)
				.ToDictionary(o => o.FollowerId);

			// 将 UserInfos 转换为 Dictionary
			var userInfosDict = _context.UserInfos
				.ToDictionary(u => u.UserId);

			// 生成查询结果
			var query = userFollowersDict
				.Select(kvp => new
				{
					o = kvp.Value,
					u = userInfosDict.TryGetValue(kvp.Key, out var userInfo) ? userInfo : null
				})
				.Where(result => result.u != null); // 过滤掉没有对应 UserInfos 的结果

			ViewBag.user = query;
			ViewBag.user_count = query.Count();

			// 预先查询所有需要的数据
			var userIds = query.Select(item => item.u.UserId).ToList();

			// 查询所有关注者数量
			var fanLists = _context.UserFollowers
				.Where(c => userIds.Contains(c.UserId) && c.UnfollowDate == null)
				.GroupBy(c => c.UserId)
				.ToDictionary(g => g.Key, g => g.ToList());

			// 查询所有食谱数量
			var fanrecipeCounts = _context.RecipeTables
				.Where(c => userIds.Contains(c.UserId))
				.GroupBy(c => c.UserId)
				.Select(g => new { UserId = g.Key, Count = g.Count() })
				.ToDictionary(x => x.UserId, x => x.Count);

			foreach (var item in query)
			{
				// 从缓存中获取关注者数量
				if (fanLists.TryGetValue(item.u.UserId, out var followList))
				{
					ViewData[$"follower_{item.u.UserId}"] = followList;
				}
				else
				{
					ViewData[$"follower_{item.u.UserId}"] = new List<UserFollower>();
				}

				// 从缓存中获取食谱数量
				if (fanrecipeCounts.TryGetValue(item.u.UserId, out var recipeCount))
				{
					ViewData[$"recipe_{item.u.UserId}"] = recipeCount;
				}
				else
				{
					ViewData[$"recipe_{item.u.UserId}"] = 0;
				}
			}

			//取得追蹤資料
			// 将 UserFollowers 转换为 Dictionary
			var followDict = _context.UserFollowers
				.Where(o => o.FollowerId == user_info.UserId && o.UnfollowDate == null)
				.OrderByDescending(o => o.FollowDate)
				.ToDictionary(o => o.UserId);

			// 生成查询结果
			var query_follow = followDict
				.Select(kvp => new
				{
					o = kvp.Value,
					u = userInfosDict.TryGetValue(kvp.Key, out var userInfo) ? userInfo : null
				})
				.Where(result => result.u != null);

			ViewBag.follow = query_follow;

			// 预先查询所有需要的数据
			var userIds_follow = query_follow.Select(item => item.u.UserId).ToList();

			// 查询所有关注者数量
			var followLists = _context.UserFollowers
				.Where(c => userIds.Contains(c.UserId) && c.UnfollowDate == null)
				.GroupBy(c => c.UserId)
				.ToDictionary(g => g.Key, g => g.ToList());

			// 查询所有食谱数量
			var followrecipeCounts = _context.RecipeTables
				.Where(c => userIds.Contains(c.UserId))
				.GroupBy(c => c.UserId)
				.Select(g => new { UserId = g.Key, Count = g.Count() })
				.ToDictionary(x => x.UserId, x => x.Count);

			foreach (var item in query_follow)
			{
				// 从缓存中获取关注者数量
				if (followLists.TryGetValue(item.u.UserId, out var followList))
				{
					ViewData[$"follow_{item.u.UserId}"] = followList;
				}
				else
				{
					ViewData[$"follow_{item.u.UserId}"] = new List<UserFollower>();
				}

				// 从缓存中获取食谱数量
				if (followrecipeCounts.TryGetValue(item.u.UserId, out var recipeCount))
				{
					ViewData[$"follow_recipe_{item.u.UserId}"] = recipeCount;
				}
				else
				{
					ViewData[$"follow_recipe_{item.u.UserId}"] = 0;
				}
			}

			var query_recipe = from r in _context.RecipeTables
							   where r.UserId == user_info.UserId
							   select r;

			ViewBag.recipe = query_recipe;
			ViewBag.recipe_count = query_recipe.Count();

			var recipeIds = query_recipe.Select(r => r.RecipeId).ToList();
			var recipeViews = _context.RecipeViews
									  .Where(v => recipeIds.Contains(v.RecipeId))
									  .ToList();

			// 计算每个食谱的视图数量
			var recipeViewCounts = recipeViews
				.GroupBy(v => v.RecipeId)
				.ToDictionary(g => g.Key, g => g.Sum(v => v.ViewNum));

			foreach (var item in query_recipe)
			{
				// 从缓存中获取视图数量
				if (recipeViewCounts.TryGetValue(item.RecipeId, out var viewCount))
				{
					ViewData[$"recipe_view_{item.RecipeId}"] = viewCount;
				}
				else
				{
					ViewData[$"recipe_view_{item.RecipeId}"] = 0;
				}
			}
			stopwatch.Stop();
			Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
			Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
			Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
			return View(user_info);
		}
	}
}
