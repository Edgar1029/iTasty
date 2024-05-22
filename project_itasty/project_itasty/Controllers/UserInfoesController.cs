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
						   join u in _context.UserInfos on o.FollowerId equals u.Id
						   orderby o.FollowDate descending
						   select new { o, u };

			ViewBag.user = query;
            foreach (var item in query)
            {
                var count = from c in _context.UserFollowers
							where c.UserId == item.u.Id
							select c;
				ViewData[$"follower_{item.u.Id}"] = count;
            }
            return View(userinfo);
		}

		// GET: UserInfoes/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userInfo = await _context.UserInfos
				.FirstOrDefaultAsync(m => m.Id == id);
			if (userInfo == null)
			{
				return NotFound();
			}

			return View(userInfo);
		}

		// GET: UserInfoes/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: UserInfoes/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Photo")] UserInfo userInfo)
		{
			if (ModelState.IsValid)
			{
				_context.Add(userInfo);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(userInfo);
		}

		// GET: UserInfoes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userInfo = await _context.UserInfos.FindAsync(id);
			if (userInfo == null)
			{
				return NotFound();
			}
			return View(userInfo);
		}

		// POST: UserInfoes/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Photo")] UserInfo userInfo)
		{
			if (id != userInfo.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(userInfo);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!UserInfoExists(userInfo.Id))
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
			return View(userInfo);
		}

		// GET: UserInfoes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userInfo = await _context.UserInfos
				.FirstOrDefaultAsync(m => m.Id == id);
			if (userInfo == null)
			{
				return NotFound();
			}

			return View(userInfo);
		}

		// POST: UserInfoes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var userInfo = await _context.UserInfos.FindAsync(id);
			if (userInfo != null)
			{
				_context.UserInfos.Remove(userInfo);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool UserInfoExists(int id)
		{
			return _context.UserInfos.Any(e => e.Id == id);
		}
	}
}
