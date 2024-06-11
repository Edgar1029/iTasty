using project_itasty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace project_itasty.Controllers
{
    public class FolderController : Controller
    {
        private ITastyDbContext _context;

        public FolderController(ITastyDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult favoriteFolder()
        {
            return View();
        }
	}
}

