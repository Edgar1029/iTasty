using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project_itasty.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace project_itasty.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ITastyDbContext _context;

        public SeasonController(ITastyDbContext context)
        {
            _context = context;
        }
        private int month = DateTime.Now.Month;
        
        // GET: Season
        public async Task<IActionResult> Index()
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userEmail != null)
            {

                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail == permission?.UserEmail && permission.UserPermissions == 1)
                {

                    //ViewBag.Month = month;
                    ViewBag.Month = 7;
                    var query2 = from o in _context.SeasonalIngredients
                                where o.MonthId == 7 /*month*/
                                select o;

                    return View(await query2.ToListAsync());
                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");
            
        }

        [HttpPost]
        public ActionResult Index(List<int> selectedIngredients_ ,List<bool> selectedIngredients) 
        {
           
            
              
            for(int i = 0; i < selectedIngredients_.Count; i++)
            {
                var ingredient = _context.SeasonalIngredients.Find(selectedIngredients_[i]);
                
                    ingredient.IsActive = selectedIngredients[i]; // 设置为 true，你可以设置为任何你需要的值

              
            }
         
            _context.SaveChanges();
           
            return Redirect("Background_control/Index");
        }



        // GET: Season/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userEmail != null)
            {

                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail == permission?.UserEmail && permission.UserPermissions == 1)
                {

                    if (id == null)
                    {
                        return NotFound();
                    }

                    var seasonalIngredient = await _context.SeasonalIngredients
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (seasonalIngredient == null)
                    {
                        return NotFound();
                    }

                    return View(seasonalIngredient);
                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");

            
        }

        // GET: Season/Create
        public IActionResult Create()
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userEmail != null)
            {

                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail == permission?.UserEmail && permission.UserPermissions == 1)
                {

                    return View();
                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");
            
        }

        // POST: Season/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( SeasonalIngredient seasonalIngredient, IFormFile file,int MonthId, string CommonName)
        {
                
            if (ModelState.IsValid)
            {
                
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    seasonalIngredient = new SeasonalIngredient
                    {

						IngredientsImg = memoryStream.ToArray()
                    };
                    seasonalIngredient.MonthId = MonthId;
                    seasonalIngredient.CommonName = CommonName;
                    seasonalIngredient.IsActive = false;
                    _context.SeasonalIngredients.Add(seasonalIngredient);
                    await _context.SaveChangesAsync();
                }
               
               
                return RedirectToAction(nameof(Index));
            }
            return View(seasonalIngredient);
        }

        

        // GET: Season/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userEmail != null)
            {

                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail == permission?.UserEmail && permission.UserPermissions == 1)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var seasonalIngredient = await _context.SeasonalIngredients
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (seasonalIngredient == null)
                    {
                        return NotFound();
                    }
                   
                    return View(seasonalIngredient);

                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");

           
        }

        // POST: Season/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seasonalIngredient = await _context.SeasonalIngredients.FindAsync(id);
            if (seasonalIngredient != null)
            {
                _context.SeasonalIngredients.Remove(seasonalIngredient);
            }

            await _context.SaveChangesAsync();
            return Redirect("/Background_control/Index");
           
            //return RedirectToAction(nameof(Index));
        }

        private bool SeasonalIngredientExists(int id)
        {
            return _context.SeasonalIngredients.Any(e => e.Id == id);
        }
    }
}
