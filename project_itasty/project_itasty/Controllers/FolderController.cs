using Final_project_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Final_project_test.Controllers
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

