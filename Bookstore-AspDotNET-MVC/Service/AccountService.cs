using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class AccountService: IAccountService
    {
        private readonly BOOKSTOREContext _context;

        public AccountService(BOOKSTOREContext context)
        {
            _context = context;
        }

        public AccountPagineDTO GetAccounts(int currentPage)
        {
            int maxRows = 10;
            var users = _context.Userinfors.ToList();

            AccountPagineDTO accountPagine = new AccountPagineDTO();

            accountPagine.Userinfors = users.OrderBy(u => u.UserId)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();

            double pageCount = (double)((decimal)users.Count() / Convert.ToDecimal(maxRows));

            accountPagine.PageCount = (int)Math.Ceiling(pageCount);

            accountPagine.CurrentPageIndex = currentPage;

            return accountPagine;
        }

        public Userinfor findUserById(long id)
        {
            return _context.Userinfors.Find(id);
        }

        public async Task<bool> addUser(Userinfor user)
        {
            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("Lỗi thêm user ",e); 
                
            }
            
        }

        public async Task<bool> updateUser(Userinfor user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteUser(Userinfor user)
        {
            try
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public long getTotalUser()
        {
            return _context.Userinfors.Count();
        }

        public string checkUserExist(Userinfor userinfor)
        {
            if (_context.Userinfors.Where(u => u.Username == userinfor.Username).Count() > 0)
            {
                return "Username đã tồn tại";
            }else if (_context.Userinfors.Where(u => u.Phone == userinfor.Phone).Count() > 0 && userinfor.Phone!=null)
            {
                return "SĐT đã tồn tại";
            }
            else if (_context.Userinfors.Where(u => u.Email == userinfor.Email).Count() > 0 && userinfor.Email != null)
            {
                return "Email đã tồn tại";
            }

            return "";
        }

        public string checkUserUpdateExist(Userinfor userinfor)
        {
            if (_context.Userinfors.Where(u => u.Username == userinfor.Username && u.UserId!=userinfor.UserId).Count() > 0)
            {
                return "Username đã tồn tại";
            }
            else if (_context.Userinfors.Where(u => u.Phone == userinfor.Phone && u.UserId != userinfor.UserId).Count() > 0 && userinfor.Phone != null)
            {
                return "SĐT đã tồn tại";
            }
            else if (_context.Userinfors.Where(u => u.Email == userinfor.Email && u.UserId != userinfor.UserId).Count() > 0 && userinfor.Email != null)
            {
                return "Email đã tồn tại";
            }
            return "";
        }

    }
}
