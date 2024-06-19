using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_itasty.Models;
using System.Linq;
using System.Threading.Tasks;

namespace project_itasty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportAPIController : ControllerBase
    {
        private readonly ITastyDbContext _context;

        public ReportAPIController(ITastyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostReport([FromBody] ReportTable report)
        {

            try
            {
                Console.WriteLine(report);
                _context.ReportTables.Add(report);
                await _context.SaveChangesAsync();

                var responseObj = new
                {
                    report.ReportId,
                    report.RecipedIdOrCommentId,
                    report.ReportedUserId,
                    report.ReportType,
                    report.ReportReason,
                    report.ReportStatus,
                    report.ReportUserId
                };

                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "內部伺服器錯誤: " + ex.Message);
            }
        }
    }
}
