using IPChecker.Models;
using Microsoft.EntityFrameworkCore;

namespace IPChecker.Repositories
{
    public interface ICountryRepository
    {
        Task Create(Country country);
        Task<Country?> GetByTwoLetterCode(string twoLetterCode);
    }

    public class CountryRepository : ICountryRepository
    {

        private readonly IpcheckerDbContext _dbContext;

        public CountryRepository(IpcheckerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Country?> GetByTwoLetterCode(string twoLetterCode)
        {
            return await _dbContext.Countries.SingleOrDefaultAsync(x => x.TwoLetterCode == twoLetterCode);
        }

        public async Task Create(Country country)
        {
            country.CreatedAt = DateTime.UtcNow;
            _dbContext.Countries.Add(country);
            await _dbContext.SaveChangesAsync();
        }
    }
}
