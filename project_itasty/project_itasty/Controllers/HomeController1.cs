using Microsoft.AspNetCore.Mvc;
using project_itasty.Models;

namespace project_itasty.Controllers
{
	public class HomeController1 : Controller
	{
		private readonly ITastyDbContext _context;

		public HomeController1(ITastyDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
