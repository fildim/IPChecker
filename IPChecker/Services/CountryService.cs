using IPChecker.Models;
using IPChecker.Repositories;

namespace IPChecker.Services
{
    public interface ICountryService
    {
        Task<Country?> GetCountryByTwoLetterCode(string twoLetterCode);
        Task Set(Country country);
    }

    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country?> GetCountryByTwoLetterCode(string twoLetterCode)
        {
            return await _countryRepository.GetByTwoLetterCode(twoLetterCode);
        }

        public async Task Set(Country country)
        {
            await _countryRepository.Create(country);
        }
    }
}
