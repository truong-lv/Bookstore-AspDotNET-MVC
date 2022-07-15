using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.IService
{
    public interface IAddressService
    {
        List<Province> GetProvinces();
        List<District> GetDistricts();
        List<District> findDistrict(long provinceId );
        List<Ward> GetWards();
        List<Ward> findWard(long districtId);

        long createAddressDetail(string addressName, long wardId);
    }
}
