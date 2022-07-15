using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class AddressService : IAddressService
    {
        private readonly BOOKSTOREContext _context;

        public AddressService(BOOKSTOREContext context)
        {
            _context = context;
        }
        public List<District> findDistrict(long provinceId)
        {
            return _context.Districts.Where(d => d.ProvinceId == provinceId).ToList();
        }

        public List<Ward> findWard(long districtId)
        {
            return _context.Wards.Where(w => w.DistrictId == districtId).ToList();
        }

        public List<District> GetDistricts()
        {
            return _context.Districts.ToList();
        }

        public List<Province> GetProvinces()
        {
            return _context.Provinces.ToList();
        }

        public List<Ward> GetWards()
        {
            return _context.Wards.ToList();
        }

        public long createAddressDetail(string addressName, long wardId)
        {
            AddressDetail addressDetail = new AddressDetail();
            addressDetail.AddressName = addressName;
            addressDetail.WarrdId = wardId;
            _context.Add(addressDetail);
             _context.SaveChanges();
            long id=_context.AddressDetails.Max(a=>a.AddressId);
            return id;

        }
    }
}
