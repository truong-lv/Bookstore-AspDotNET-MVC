using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly BOOKSTOREContext _context;

        public CategoryService(BOOKSTOREContext context)
        {
            _context = context;
        }

        public List<Category> getAllCategory()
        {
            return _context.Categories.ToList();
        }
    }
}
