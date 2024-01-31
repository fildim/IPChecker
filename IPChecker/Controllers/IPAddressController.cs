using AutoMapper;
using FluentValidation;
using IPChecker.DTOS.SearchIPAddressDTOS;
using IPChecker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IPChecker.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class IPAddressController : ControllerBase
    {
        private readonly IIpAddressService _ipAddressService;
        private readonly IMapper _mapper;
        private readonly IValidator<InputIPAddressDTO> _validator;

        public IPAddressController(IIpAddressService ipAddressService, IMapper mapper, IValidator<InputIPAddressDTO> validator)
        {
            _ipAddressService = ipAddressService;
            _mapper = mapper;
            _validator = validator;
        }


    }
}
