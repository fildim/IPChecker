using AutoMapper;
using IPChecker.DTOS.SearchIPAddressDTOS;
using IPChecker.Models;

namespace IPChecker.Mappers
{
    public class IPAddressMapper : Profile
    {
        public IPAddressMapper() 
        {
            CreateMap<InputIPAddressDTO, IpAddress>();

        }
    }
}
