using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_itasty.Models;

namespace project_itasty.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserApiController : ControllerBase
	{
		private readonly ITastyDbContext _context;

		public UserApiController(ITastyDbContext context)
		{
			_context = context;
		}

		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<UserInfo>>> GetTodoItemList()
		//{
		//	return await _context.UserInfos.ToListAsync();
		//}

		[HttpGet("follower/{id}")]
		public async Task<ActionResult<IEnumerable<UserFollower>>> GetFollower(int id)
		{
			var query = from o in _context.UserFollowers
						where o.UserId == id
						select o;
			return await query.ToListAsync();
		}

		[HttpGet("recipeview/{id}")]
		public async Task<ActionResult<IEnumerable<RecipeView>>> GetRecipeView(int id)
		{
			var query = from o in _context.RecipeViews
						where o.RecipeId == id
						select o;
			return await query.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult> UpdateUser(IFormFile photo, IFormFile banner, [FromForm] string name, [FromForm] string id, [FromForm] string intro = "")
		{
			byte[] fileBytes_photo;
			using (var memorystream = new MemoryStream())
			{
				photo.CopyTo(memorystream);
				fileBytes_photo = memorystream.ToArray();
			}
			byte[] fileBytes_banner;
			using (var memorystream = new MemoryStream())
			{
				banner.CopyTo(memorystream);
				fileBytes_banner = memorystream.ToArray();
			}

			//UserInfo userInfo = new UserInfo()
			//{
			//	UserId = int.Parse(id),
			//	UserName = name,
			//	UserPhoto = fileBytes
			//};

			UserInfo? userInfo = _context.UserInfos.Find(int.Parse(id));
			if (userInfo != null)
			{
				userInfo.UserName = name;
				userInfo.UserIntro = intro;
				userInfo.UserPhoto = fileBytes_photo;
				userInfo.UserBanner = fileBytes_banner;

				_context.Entry(userInfo).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}

			return Ok(name);
		}

		[HttpPost]
		public async Task<ActionResult> UpdateFollower()
		{
			int user_id = int.Parse(Request.Form["user_id"]);
			int follower_id = int.Parse(Request.Form["follower_id"]);
			var query = from u in _context.UserFollowers
						where u.UnfollowDate == null & u.UserId == user_id & u.FollowerId == follower_id
						select u;

			UserFollower? userFollower = await query.FirstOrDefaultAsync();
			if (userFollower != null)
			{
				userFollower.UnfollowDate = DateOnly.FromDateTime(DateTime.Now);
				_context.Entry(userFollower).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}
			else
			{
				query = from u in _context.UserFollowers
						where u.UnfollowDate != null & u.UserId == user_id & u.FollowerId == follower_id & u.FollowDate == DateOnly.FromDateTime(DateTime.Now)
						select u;
				userFollower = await query.FirstOrDefaultAsync();
				if (userFollower != null)
				{
					userFollower.UnfollowDate = null;
					_context.Entry(userFollower).State = EntityState.Modified;
					await _context.SaveChangesAsync();
				}
				else
				{
					userFollower = new UserFollower()
					{
						UserId = user_id,
						FollowerId = follower_id,
						FollowDate = DateOnly.FromDateTime(DateTime.Now),
						UnfollowDate = null
					};
					_context.UserFollowers.Add(userFollower);
					await _context.SaveChangesAsync();
				}
			}

			return Ok(userFollower.UnfollowDate);
		}
	}
}
