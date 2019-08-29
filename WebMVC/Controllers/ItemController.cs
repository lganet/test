using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Service;

namespace WebMVC.Controllers
{
    public class ItemController : Controller
    {
        public async Task<IActionResult> Index(string item)
        {
            var itemApi = new ItemApi();

            var items = await itemApi.Get(item);

            return View(items);
        }

        public IActionResult New(int id, string name, int price, string errorMessage)
        {
            var item = new ItemViewModel() { Id = id, Name = name, Price = price };

            ViewBag.ErrorMessage = errorMessage;

            return View(item);
        }

        public async Task<IActionResult> Save(ItemViewModel itemViewModel)
        {
            var itemApi = new ItemApi();

            try
            {
                await itemApi.Add(itemViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("New", new { id = itemViewModel.Id, name = itemViewModel.Name, price = itemViewModel.Price, errorMessage = ex.Message });
            }

            return RedirectToAction("Index");
        }
    }
}