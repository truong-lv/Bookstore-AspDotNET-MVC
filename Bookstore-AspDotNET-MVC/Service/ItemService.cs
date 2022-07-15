using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.Models.ModelView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class ItemService : IItemService
    {
        private readonly BOOKSTOREContext _context;
        private readonly IBookService bookService;
        private readonly IAddressService addressService;
        public ItemService(BOOKSTOREContext context, IBookService bookService, IAddressService addressService)
        {
            _context = context;
            this.bookService = bookService;
            this.addressService = addressService;
        }
        public async Task<bool> addItem(Item item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteItem(Item item)
        {
            try
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public UserItem GetUserItems(long idUser)
        {
            UserItem userItem = new UserItem();
            List<Item> items= _context.Items.Where(i => i.UserId == idUser).ToList();
            foreach(Item item in items)
            {
                userItem.itemDTOs.Add(new ItemDTO()
                {
                    item = item,
                    bookDTO = bookService.conveBookDTO(item.IdBook)
                });
            }

            userItem.provinces = addressService.GetProvinces();
            userItem.districts = addressService.findDistrict(userItem.provinces.ElementAt(0).ProvinceId);
            userItem.wards = addressService.findWard(userItem.districts.ElementAt(0).DistrictId);
            userItem.payments = _context.Payments.Where(p=>p.PaymentStatus==1).ToList();

            return userItem;
        }

        public Item findItemById(long idBook, long idUser)
        {
            return _context.Items.Where(i => i.IdBook == idBook && i.UserId == idUser).FirstOrDefault();
        }

        public async Task<bool> updateItem(Item item)
        {
            try
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> deleteUserItem(long idUser)
        {
            try
            {
                List<Item> items = _context.Items.Where(i => i.UserId == idUser).ToList();
                foreach(Item item in items)
                {
                    _context.Remove(item);
                }
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
