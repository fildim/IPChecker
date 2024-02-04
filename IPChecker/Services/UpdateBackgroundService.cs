using Hangfire;
using IPChecker.Models;
using Microsoft.EntityFrameworkCore;

namespace IPChecker.Services
{
    public class UpdateBackgroundService
    {

        private readonly IpcheckerDbContext _dbContext;
        private readonly IP2CService _iP2CService;
        private readonly MemoryCacheService _memoryCacheService;

        public UpdateBackgroundService(
            IpcheckerDbContext dbContext, IP2CService iP2CService, MemoryCacheService memoryCacheService)
        {
            _dbContext = dbContext;
            _iP2CService = iP2CService;
            _memoryCacheService = memoryCacheService;
        }

        public void Update()
        {

            var now = DateTime.UtcNow;
            var batch = 100;
            var skip = 0;

            var ipBatch = _dbContext.IpAddresses.Include(x => x.Country).OrderBy(x => x.Id).ToList();

            // transaction??

            while (true)
            {

                var queried = ipBatch.Skip(skip).Take(batch).ToList();

                if (queried.Count() == 0) break;

                queried.ForEach(async x =>
                    {
                        var ip = await _iP2CService.OnGet(x.Ip);

                        if (x.Country.Name != ip.CountryName || x.Country.TwoLetterCode != ip.TwoLetterCode || x.Country.ThreeLetterCode != ip.ThreeLetterCode)
                        {
                            x.Country.Name = ip.CountryName;
                            x.Country.TwoLetterCode = ip.TwoLetterCode;
                            x.Country.ThreeLetterCode = ip.ThreeLetterCode;
                            x.UpdatedAt = now;

                            _dbContext.SaveChanges();

                            _memoryCacheService.Remove(x);
                        }
                    }
                );

                skip += batch;
            }


        }
    }
}
