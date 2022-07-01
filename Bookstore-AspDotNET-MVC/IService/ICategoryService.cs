using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.IService
{
    public interface ICategoryService
    {
        List<Category> getAllCategory();


    }
}
