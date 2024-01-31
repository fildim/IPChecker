using IPChecker.Exceptions;
using IPChecker.Models;
using IPChecker.Repositories;

namespace IPChecker.Services
{
    public interface IIpAddressService
    {
        Task<IpAddress> GetByIp(IpAddress ipAddress);
    }

    public class IpAddressService : IIpAddressService
    {
        private readonly IIpAddressRepository _ipAddressRepository;

        public IpAddressService(IIpAddressRepository ipAddressRepository)
        {
            _ipAddressRepository = ipAddressRepository;
        }

        public async Task<IpAddress> GetByIp(IpAddress ipAddress)
        {
            var ip = await _ipAddressRepository.GetByIp(ipAddress);

            if (ip == null)
            {
                throw new IPAddressNotFoundException("IP Address Not Found");
            }

            return ip;
        }

    }
}
