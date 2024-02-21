using Hangfire;
using IPChecker.Models;
using Microsoft.EntityFrameworkCore;

namespace IPChecker.Services
{
    public class UpdateBackgroundService
    {

        private readonly IpcheckerDbContext _dbContext;
        private readonly IIP2CService _iP2CService;
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly ILogger<UpdateBackgroundService> _logger;

        public UpdateBackgroundService(
            IpcheckerDbContext dbContext,
            IIP2CService iP2CService,
            IMemoryCacheService memoryCacheService,
            ILogger<UpdateBackgroundService> logger)
        {
            _dbContext = dbContext;
            _iP2CService = iP2CService;
            _memoryCacheService = memoryCacheService;
            _logger = logger;
        }

        public async Task Update()
        {
            try
            {
                var now = DateTime.UtcNow;
                var batch = 100;
                var skip = 0;

                while (true)
                {

                    var ipBatch = await _dbContext.IpAddresses
                        .Include(x => x.Country)
                        .OrderBy(x => x.Id)
                        .Skip(skip)
                        .Take(batch)
                        .ToListAsync();

                    if (ipBatch.Count() == 0) break;

                    ipBatch.ForEach(async x =>
                        {
                            var ip = await _iP2CService.OnGet(x.Ip);

                            if
                                (
                                x.Country.Name != ip.CountryName
                                || x.Country.TwoLetterCode != ip.TwoLetterCode
                                || x.Country.ThreeLetterCode != ip.ThreeLetterCode
                                )
                            {
                                x.Country.Name = ip.CountryName;
                                x.Country.TwoLetterCode = ip.TwoLetterCode;
                                x.Country.ThreeLetterCode = ip.ThreeLetterCode;
                                x.UpdatedAt = now;

                                _memoryCacheService.Remove(x);

                                await _dbContext.SaveChangesAsync();
                            }
                        }
                    );

                    skip += batch;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
