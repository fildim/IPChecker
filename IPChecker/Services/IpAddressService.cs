using IPChecker.Exceptions;
using IPChecker.Models;
using IPChecker.Repositories;

namespace IPChecker.Services
{
    public interface IIpAddressService
    {
        Task<IpAddress?> GetByIp(string ipAddress);
        Task Set(IpAddress ipAddress);
    }

    public class IpAddressService : IIpAddressService
    {
        private readonly IIpAddressRepository _ipAddressRepository;

        public IpAddressService(IIpAddressRepository ipAddressRepository)
        {
            _ipAddressRepository = ipAddressRepository;
        }

        public async Task<IpAddress?> GetByIp(string ipAddress)
        {
            var ip = await _ipAddressRepository.GetByIp(ipAddress);

            return ip;
        }

        public async Task Set(IpAddress ipAddress)
        {
            await _ipAddressRepository.Create(ipAddress);
        }

    }
}
