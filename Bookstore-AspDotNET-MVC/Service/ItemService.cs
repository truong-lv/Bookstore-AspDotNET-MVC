using Bookstore_AspDotNET_MVC.Data;
using Bookstore_AspDotNET_MVC.IService;
using Bookstore_AspDotNET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Service
{
    public class ItemService : IItemService
    {
        private readonly BOOKSTOREContext _context;

        public ItemService(BOOKSTOREContext context)
        {
            _context = context;
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

        public List<Item> GetUserItems(long idUser)
        {
            return _context.Items.Where(i => i.UserId == idUser).ToList();
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
    }
}
