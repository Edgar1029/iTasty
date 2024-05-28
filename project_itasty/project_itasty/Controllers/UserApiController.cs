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

		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserInfo>>> GetTodoItemList()
		{
			return await _context.UserInfos.ToListAsync();
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
	}
}
