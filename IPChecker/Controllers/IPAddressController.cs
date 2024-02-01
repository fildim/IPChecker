using AutoMapper;
using FluentValidation;
using IPChecker.DTOS.CountryDTOS;
using IPChecker.DTOS.SearchIPAddressDTOS;
using IPChecker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IPChecker.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class IPAddressController : ControllerBase
    {
        private readonly IMyService _myService;

        public IPAddressController(IValidator<InputIPAddressDTO> validator, IMyService myService)
        {
            _myService = myService;
        }

        [HttpGet("[action]/{ipAddress}")]
        public async Task<ActionResult<OutputCountryDTO>> Get ([FromRoute]string ipAddress)
        {
            var ip = await _myService.Get(ipAddress);

            if (ip == null)
            {
                return NotFound("Ip Address Not Found");
            }
            else
            {
                return Ok(ip);
            }
        } 
    }
}
