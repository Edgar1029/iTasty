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

		[HttpPost("{id}")]
		public async Task<ActionResult> PutUserInfo(int id, UserInfo userInfo)
		{
			_context.Entry(userInfo).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return Ok(userInfo);
		}

		[HttpPost("test")]
        public async Task<ActionResult> UploadImage(IFormFile photo, [FromForm] string name, [FromForm] string id)
        {
			byte[] fileBytes;
			using (var memorystream  = new MemoryStream())
			{
				photo.CopyTo(memorystream);
				fileBytes = memorystream.ToArray();
			}

			UserInfo userInfo = new UserInfo()
			{
				Id = int.Parse(id),
				Name = name,
                Photo = fileBytes
            };

            _context.Entry(userInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(name);
		}
	}
}
