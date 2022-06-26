using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class AuthorService
    {
        private readonly BOOKSTOREContext _context;

        public AuthorService(BOOKSTOREContext context)
        {
            _context = context;
        }

        public List<Author> getAllAuthor()
        {
            return _context.Authors.ToList();
        }
    }
}
