using Bookstore_AspDotNET_MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.IService;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class CompanyService: ICompanyService
    {
        private readonly BOOKSTOREContext _context;

        public CompanyService(BOOKSTOREContext context)
        {
            _context = context;
        }

        public List<PublishingCompany> getAllCompany()
        {
            return _context.PublishingCompanies.ToList();
        }
    }
}
