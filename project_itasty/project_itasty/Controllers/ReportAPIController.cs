using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_itasty.Models;
using System.Composition;

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
                _context.ReportTables.Add(report);
                int rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected > 0)
                {
                    return Ok("Report submitted successfully.");
                }
                else
                {
                    return BadRequest("Failed to submit report.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
