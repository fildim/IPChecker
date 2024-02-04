using IPChecker.DTOS.ReportDTOS;
using IPChecker.Models;

namespace IPChecker.Services
{
    public interface IReportService
    {
        Task<List<ReportDTO>> Get(string[] codes);
    }

    public class ReportService : IReportService
    {
        private readonly IpcheckerDbContext _dbContext;

        public ReportService(IpcheckerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ReportDTO>> Get(string[] codes)
        {

            if (codes.Length == 0)
            {
                
            }


            return new List<ReportDTO>() { };
        }
    }
}
