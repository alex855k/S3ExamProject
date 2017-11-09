using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TryAndDie.Model.WebCrawlers;

namespace TryAndDie.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        
        public SearchController()
        {
            
        }
       [HttpGet("[action]")]
        public IEnumerable<Item> GetItems()
        {
            RvuniqueCrawler.Run();
            //var items = new[]
            //{
            //     new Item
            //     {
            //         Code = 666,
            //         Name = "Devil",
            //         Price = 666,
            //         Supplier = "Molly"
            //     },
            //     new Item
            //     {
            //         Code = 777,
            //         Name = "Black Cat",
            //         Price = 12,
            //         Supplier = "Omen"
            //     }
            //};
           List<Item> items = new List<Item>();
           for(int i = 0; i<50; i++)
           {
                items.Add(new Item
                {
                    Code = i,
                    Name = "Black Cat",
                    Price = 12,
                    Supplier = "Omen"
                });
            }
            return items;
           
        }

        public class Item
        {
            public string Category { get; set; }
            public int Code { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public string Supplier { get; set; }
            public Uri Link { get; set; }
        }

        //// GET: Search/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Search/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Search/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Search/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Search/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Search/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Search/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}