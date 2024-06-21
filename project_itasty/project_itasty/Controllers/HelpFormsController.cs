using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_itasty.Models;

namespace project_itasty.Controllers
{
    public class HelpFormsController : Controller
    {
        private readonly ITastyDbContext _context;

        public HelpFormsController(ITastyDbContext context)
        {
            _context = context;
        }

        // GET: HelpForms
        public async Task<IActionResult> Index()
        {
            string userEmail = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userEmail != null)
            {

                var query = from o in _context.UserInfos where o.UserEmail == userEmail select o;
                var permission = query.FirstOrDefault();

                if (userEmail == permission?.UserEmail && permission.UserPermissions == 1)
                {

                    var iTastyDb01Context = _context.HelpForms.Include(h => h.User);
                    return View(await iTastyDb01Context.ToListAsync());
                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");
           
        }

        // GET: HelpForms/Details/5
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

                    var helpForm = await _context.HelpForms
                        .Include(h => h.User)
                        .FirstOrDefaultAsync(m => m.FormId == id);
                    if (helpForm == null)
                    {
                        return NotFound();
                    }

                    return View(helpForm);

                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");
           
        }

        // GET: HelpForms/Create
        public IActionResult Create()
        {
            TempData["message"] = null;
            string userName = HttpContext.Session.GetString("userEmail") ?? "Guest";
            if (userName == "Guest")
            {
                return Redirect("/UserRegister/Create");
            }
            ViewData["UserId"] = new SelectList(_context.UserInfos, "Id", "Id");
            return View();

            
        }

        // POST: HelpForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HelpForm helpForm, int UserId, string QuestionType, string QuestionContent, IFormFile file)
        {
           
            if (file != null && file.Length > 0)
            //if (ModelState.IsValid)
            {
               
                
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                          helpForm = new HelpForm
                        {
                            UserId = UserId,
                            QuestionType = QuestionType,
                            QuestionImage = memoryStream.ToArray()

                        };
                           
                    }
                        helpForm.QuestionContent = QuestionContent;
                        _context.HelpForms.Add(helpForm);
                        await _context.SaveChangesAsync();
                TempData["message"] = "表單傳送完成";
                return View("Create");
                //return RedirectToAction("Index","Home");


            }
            else
            {

                helpForm.UserId = UserId;
                helpForm.QuestionType = QuestionType;
                helpForm.QuestionImage = null;
                helpForm.QuestionContent = QuestionContent;
                _context.HelpForms.Add(helpForm);
                await _context.SaveChangesAsync();
                TempData["message"] = "表單傳送完成";
                return View("Create");
            }
            ViewData["UserId"] = new SelectList(_context.UserInfos, "Id", "Id", helpForm.UserId);
            return Content("error");
        }

        // GET: HelpForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

                    var helpForm = await _context.HelpForms.FindAsync(id);
                    if (helpForm == null)
                    {
                        return NotFound();
                    }
                    ViewData["UserId"] = new SelectList(_context.UserInfos, "Id", "Id", helpForm.UserId);
                    return View(helpForm);

                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");

           
        }

        // POST: HelpForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormId,UserId,QuestionType,QuestionContent")] HelpForm helpForm)
        {
            if (id != helpForm.FormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helpForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelpFormExists(helpForm.FormId))
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
            ViewData["UserId"] = new SelectList(_context.UserInfos, "Id", "Id", helpForm.UserId);
            return View(helpForm);
        }

        // GET: HelpForms/Delete/5
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

                    var helpForm = await _context.HelpForms
                        .Include(h => h.User)
                        .FirstOrDefaultAsync(m => m.FormId == id);
                    if (helpForm == null)
                    {
                        return NotFound();
                    }

                    return View(helpForm);

                }
                else
                {

                    return Redirect("/Home/Index");

                }
            }

            return Redirect("/Home/Index");
           
        }

        // POST: HelpForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var helpForm = await _context.HelpForms.FindAsync(id);
            if (helpForm != null)
            {
                _context.HelpForms.Remove(helpForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HelpFormExists(int id)
        {
            return _context.HelpForms.Any(e => e.FormId == id);
        }
    }
}
