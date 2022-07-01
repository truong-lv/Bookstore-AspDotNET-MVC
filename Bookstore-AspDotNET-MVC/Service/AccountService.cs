﻿using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class AccountService
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

    }
}
