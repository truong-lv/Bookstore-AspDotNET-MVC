using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_AspDotNET_MVC.Controllers.Admin
{
    public class OrderManagerController : Controller
    {
        // GET: OrderManagerController
        public ActionResult Index()
        {
            ViewData["Order"] = "active";
            return View("/Views/Admin/Order/Index.cshtml");
        }

        // GET: OrderManagerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderManagerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderManagerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
