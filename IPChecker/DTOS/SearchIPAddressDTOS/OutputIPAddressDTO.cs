using IPChecker.DTOS.CountryDTOS;

namespace IPChecker.DTOS.SearchIPAddressDTOS
{
    public class OutputIPAddressDTO
    {
        public int Id { get; set; }
        public OutputCountryDTO Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
