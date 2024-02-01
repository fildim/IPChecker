using IPChecker.DTOS.CountryDTOS;
using IPChecker.Models;

namespace IPChecker.Services
{
    public interface IMyService
    {
        Task<OutputCountryDTO?> Get(string ipAddress);
    }

    public class MyService : IMyService
    {
        private readonly IIP2CService _service;
        private readonly IIpAddressService _ipAddressService;
        private readonly IMemoryCacheService _memoryCacheService;

        public MyService(IIP2CService service, IIpAddressService ipAddressService, IMemoryCacheService memoryCacheService)
        {
            _service = service;
            _ipAddressService = ipAddressService;
            _memoryCacheService = memoryCacheService;
        }

        public async Task<OutputCountryDTO?> Get(string ipAddress)
        {
            try
            {
                var ip = _memoryCacheService.Get(ipAddress);

                if (ip == null)
                {
                    ip = await _ipAddressService.GetByIp(ipAddress);

                    if (ip != null)
                    {
                        _memoryCacheService.Set(ip);
                    }
                    else
                    {
                        var fromIP2C = await _service.Get(ipAddress);

                        ip = new IpAddress { };

                        await _ipAddressService.Set(ip);

                        _memoryCacheService.Set(ip);
                    }
                }

                return new OutputCountryDTO { Name = ip.Country.Name, TwoLetterCode = ip.Country.TwoLetterCode, ThreeLetterCode = ip.Country.ThreeLetterCode };

            }
            catch
            {
                //logging
                return null;
            }
        }


    }
}
