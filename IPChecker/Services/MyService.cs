using IPChecker.DTOS.CountryDTOS;
using IPChecker.Middleware;
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
        private readonly ICountryService _countryService;
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly ILogger<MyService> _logger;

        public MyService(IIP2CService service, IIpAddressService ipAddressService, IMemoryCacheService memoryCacheService, ICountryService countryService, ILogger<MyService> logger)
        {
            _service = service;
            _ipAddressService = ipAddressService;
            _memoryCacheService = memoryCacheService;
            _countryService = countryService;
            _logger = logger;
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
                        _memoryCacheService.Set(ip, ipAddress);
                    }
                    else
                    {
                        var fromIP2C = await _service.OnGet(ipAddress);

                        var country = await _countryService
                            .GetCountryByTwoLetterCode(fromIP2C.TwoLetterCode);

                        if (country == null)
                        {
                            country = new Country
                            {
                                Name = fromIP2C.CountryName,
                                TwoLetterCode = fromIP2C.TwoLetterCode,
                                ThreeLetterCode = fromIP2C.ThreeLetterCode
                            };

                            await _countryService.Set(country);
                        }


                        ip = new IpAddress
                        {
                            CountryId = country.Id,
                            Ip = ipAddress
                        };

                        await _ipAddressService.Set(ip);

                        _memoryCacheService.Set(ip, ipAddress);
                    }
                }

                return new OutputCountryDTO
                {
                    Name = ip.Country.Name,
                    TwoLetterCode = ip.Country.TwoLetterCode,
                    ThreeLetterCode = ip.Country.ThreeLetterCode
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }


    }
}
