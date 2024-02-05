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
        private readonly IMapper _mapper;

        public ReportsController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
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
