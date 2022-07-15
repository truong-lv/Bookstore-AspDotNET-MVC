using Bookstore_AspDotNET_MVC.DTO;
using Bookstore_AspDotNET_MVC.Models;
using Bookstore_AspDotNET_MVC.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.IService
{
    public interface IItemService
    {
        UserItem GetUserItems(long idUser);

        Item findItemById(long idBook, long idUser);

        Task<bool> addItem(Item item);

        Task<bool> updateItem(Item item);

        Task<bool> deleteItem(Item item);

        Task<bool> deleteUserItem(long idUser);

    }
}
