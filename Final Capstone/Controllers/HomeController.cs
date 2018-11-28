using Final_Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Capstone.Controllers
{
    public class HomeController : Controller
    {
        private CarContext db = new CarContext();
        //(instance of api controller)
        private CarsController api = new CarsController();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult CarSearch(string search, string text)
        {
            List<Car> results = new List<Car>();
            switch (search)
            {
                case "Make":
                    var resultMake = api.GetMake(text);
                    results = resultMake.ToList();
                    break;
                case "Model":
                    var resultModel = api.GetModel(text);
                    results = resultModel.ToList();
                    break;
                case "Year":
                    int x;
                    int.TryParse(text, out x);
                    var resultYear = api.GetYear(x);
                    results = resultYear.ToList();
                    break;
                case "Color":
                    var resultColor = api.GetColor(text);
                    results = resultColor.ToList();
                    break;
                default:
                    break;
            }

            return View(results);
        }


        
    }
}
