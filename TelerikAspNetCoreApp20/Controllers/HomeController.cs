using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelerikAspNetCoreApp20.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace TelerikAspNetCoreApp20.Controllers
{
    public class CarViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public int? InStock { get; set; }

        public bool Discontinued { get; set; }
    }
    public class HomeController : Controller
    {
    
        private readonly CarsDb db;

        public HomeController(CarsDb context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Controls()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


        public ActionResult Cars_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json((from c in db.Cars
                         select new CarViewModel
                         {
                             Id = c.Id,
                             Name = c.Name,
                             Category = c.Category,
                             Discontinued = c.Discontinued,
                             InStock = c.InStock
                         }).ToDataSourceResult(request));

        }
        [HttpPost]
        public ActionResult Cars_Create([DataSourceRequest] DataSourceRequest request, CarViewModel car)
        {
            var results = new List<CarViewModel>();
            if (car != null && ModelState.IsValid)
            {
                var newCar = new Car { Name = car.Name, Category = car.Category, Discontinued = car.Discontinued, InStock = car.InStock.Value };
                db.Cars.Add(newCar);
                db.SaveChanges();
                car.Id = newCar.Id;
                results.Add(car);
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Cars_Update([DataSourceRequest] DataSourceRequest request, CarViewModel car)
        {
            if (car != null && ModelState.IsValid)
            {
                var dbCar = db.Cars.Where(c => c.Id == car.Id).SingleOrDefault();
                if (dbCar != null)
                {
                    dbCar.Name = car.Name;
                    dbCar.Category = car.Category; dbCar.Discontinued = car.Discontinued;
                    dbCar.InStock = car.InStock.Value;

                }
            }
            db.SaveChanges();
            return Json(new[] { car }.ToDataSourceResult(request, ModelState));
        }
        [HttpPost]
        public ActionResult Cars_Destroy([DataSourceRequest] DataSourceRequest request, CarViewModel car)
        {
            var dbCar = db.Cars.Where(c => c.Id == car.Id).SingleOrDefault();
            db.Cars.Remove(dbCar);
            db.SaveChanges();
            return Json(new[] { car }.ToDataSourceResult(request));
        }
    }
}

