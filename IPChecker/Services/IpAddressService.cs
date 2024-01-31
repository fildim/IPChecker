using IPChecker.Repositories;

namespace IPChecker.Services
{
    public class IpAddressService
    {
        private readonly IIpAddressRepository _ipAddressRepository;

        public IpAddressService(IIpAddressRepository ipAddressRepository)
        {
            _ipAddressRepository = ipAddressRepository;
        }


    }
}
