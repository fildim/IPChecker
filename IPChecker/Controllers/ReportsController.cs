using AutoMapper;
using IPChecker.DTOS.ReportDTOS;
using IPChecker.Services;
using Microsoft.AspNetCore.Mvc;

namespace IPChecker.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
            
        }

        [HttpGet("[action]/{codes}")]
        public async Task<ActionResult<List<ReportDTO>>> Get ([FromRoute]string[] codes)
        {
            var report = await _reportService.Get(codes);

            if (report.Count() == 0)
            {
                return NotFound("No ip addresses found");
            }
            else
            {
                return Ok(report);
            }

        }


    }
}
