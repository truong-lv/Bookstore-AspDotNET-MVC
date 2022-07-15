using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers.Api
{
    public class AddressAPIController : Controller
    {
        private readonly ILogger<AddressAPIController> _logger;
        private readonly IAddressService addressService;
        public AddressAPIController(ILogger<AddressAPIController> logger, IAddressService addressService)
        {
            _logger = logger;
            this.addressService = addressService;
        }
        public IActionResult getDisTrict(long provinceId)
        {
            List<District> districts = addressService.findDistrict(provinceId);
            return Ok(districts);
        }

        public IActionResult getWard(long districtId)
        {
            List<Ward> wards = addressService.findWard(districtId);
            return Ok(wards);
        }
    }
}
