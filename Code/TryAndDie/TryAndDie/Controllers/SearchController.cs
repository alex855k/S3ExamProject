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
        public IEnumerable<Item> GetItems(string name)
        {
            var thisone = name;
            //RvuniqueCrawler.Run();
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
            //         Price = 88,
            //         Supplier = "Omen"
            //     },
            //     new Item
            //     {
            //         Code = 666,
            //         Name = "Black Cat",
            //         Price = 12,
            //         Supplier = "Omen"
            //     },
            //     new Item
            //     {
            //         Code = 111,
            //         Name = "Black Cat",
            //         Price = 1000,
            //         Supplier = "Omen"
            //     }
            //};
            //var sorted = items.OrderBy(i => i.Price).ToList();
            //List<Item> items = new List<Item>();
            //Random r = new Random();
            //for (int i = 0; i < 200; i++)
            //{
            //    items.Add(new Item
            //    {
            //        Code = i,
            //        Name = "Black Cat",
            //        Price = r.Next(2, 500),
            //        Supplier = "Omen"
            //    });
            //}
            //var sorted = items.OrderBy(i => i.Price);
            var items = new[]
            {
                new Item
                {
                    Image = new Uri("https://pbs.twimg.com/profile_images/3711327849/49b0247973851a06f15640d3811c39e3.jpeg"),
                    Name = "Hankey Cleaner",
                    Description = "Unique item",
                    //Lowest = 100.66M,
                    Suppliers = new List<Supplier>()
                    {
                        new Supplier
                        {
                            Url = new Uri("https://www.ebay.ie/"),
                            Name = "Ebay",
                            Price = 66M
                        },
                        new Supplier
                        {
                            Url = new Uri("https://www.amazon.ca/"),
                            Name = "Amazon",
                            Price = 44M
                        }
                    }
                },
                new Item
                {
                    Image = new Uri("https://vignette.wikia.nocookie.net/muppet/images/3/35/Miss_Piggy_season_1.jpg/revision/latest/scale-to-width-down/250?cb=20120317224031"),
                    Name = "Ms Piggy",
                    Description = "Dangerous",
                    //Lowest = 100.33M,
                    Suppliers = new List<Supplier>()
                    {
                        new Supplier
                        {
                            Url = new Uri("https://www.ebay.ie/"),
                            Name = "Ebay",
                            Price = 2M
                        },
                        new Supplier
                        {
                            Url = new Uri("https://www.amazon.ca/"),
                            Name = "Amazon",
                            Price = 10M
                        }
                    }
                }
            };
            items.ToList().ForEach(
                i =>
                    {
                        i.Lowest = i.Suppliers.OrderBy(Supplier => Supplier.Price).Select(Supplier => Supplier.Price).First();
                        var something = i.Lowest;
                    }
                );

            return items;
           
        }

        public class Item
        {
            public Uri Image { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Lowest { get; set; }
            public List<Supplier> Suppliers { get; set; }
        }

        public class Supplier
        {
            public Uri Url { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }

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