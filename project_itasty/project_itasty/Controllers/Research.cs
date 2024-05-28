using Microsoft.AspNetCore.Mvc;

namespace project_itasty.Controllers
{
    public class Research : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
