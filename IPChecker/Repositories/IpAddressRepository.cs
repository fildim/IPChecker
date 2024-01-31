using IPChecker.Models;
using Microsoft.EntityFrameworkCore;

namespace IPChecker.Repositories
{
    public interface IIpAddressRepository
    {
        Task Create(IpAddress ipAddress);
        Task<List<IpAddress>> GetAll();
        Task<IpAddress?> GetById(int id);
        Task<IpAddress?> GetByIp(IpAddress ipAddress);
        Task Update(IpAddress ipAddress);
    }



    public class IpAddressRepository : IIpAddressRepository
    {
        private readonly IpcheckerDbContext _dbContext;

        public IpAddressRepository(IpcheckerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(IpAddress ipAddress)
        {
            _dbContext.IpAddresses.Add(ipAddress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(IpAddress ipAddress)
        {
            _dbContext.IpAddresses.Update(ipAddress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IpAddress?> GetById(int id)
        {
            return await _dbContext.IpAddresses.Include(x => x.Country).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IpAddress?> GetByIp(IpAddress ipAddress)
        {
            return await _dbContext.IpAddresses.Include(x => x.Country).SingleOrDefaultAsync(x => x.Id == ipAddress.Id);
        }

        public async Task<List<IpAddress>> GetAll()
        {
            return await _dbContext.IpAddresses.ToListAsync();
        }



    }
}
