using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project_itasty.Models;
using System.Linq;

namespace project_itasty.Controllers
{
    public class Background_control : Controller
    {
        private readonly ITastyDbContext _context;
        public Background_control(ITastyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> index()
        {
            #region Chart 圖表
            var chartData1 = new Background_Control_ChartData
            {
                Labels = await _context.RecipeTables.Select(r => r.RecipeName).ToListAsync(),
                Data = await _context.RecipeTables.Select(r => r.Views).ToListAsync()
            };

            var chartData2 = new Background_Control_ChartData
            {
                Labels = new List<string> { "January", "February", "March", "April", "May", "June" },
                Data = new List<int> { 12, 19, 3, 5, 2, 3 }
            };
            #endregion

            #region 從recipedTable拿資料，並把資料灌進自建的模型(Background_Control_RecipedTable)
            var RecipedTable = new List<Background_Control_RecipedTable>();

            var recipeList = _context.RecipeTables.OrderByDescending(r => r.Views).ToList();

            foreach (var recipe in recipeList)
            {
                RecipedTable.Add(new Background_Control_RecipedTable
                {
                    RecipedName = recipe.RecipeName,
                    Author = recipe.UserId, 
                    RecipedView = recipe.Views, 
                    NumberOfComment = recipe.Favorites,
                    RecipedStatus = recipe.RecipeStatus
                });
            }
            #endregion


            var model =  new Backgroud_Control_Model
            {
                ChartViewsData = chartData1,
                ChartMembershipData = chartData2,
                RecipedTable = RecipedTable,
            };

            return View(model);

        }
    }
}
