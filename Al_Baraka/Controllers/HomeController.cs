using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Al_Baraka.Models;
using System.IO;
namespace Al_Baraka.Controllers
{
    public class HomeController : Controller
    {
        private ProductContext pc;
        public HomeController(ProductContext prodCont)
        {
            pc = prodCont;
        }
        public IActionResult Index()
        {
            return View(pc.Products.ToList());
        }
        [HttpPost]
        public IActionResult ShowOneGroup(int num)
        {
            
            switch (num)
            {
                case 1:

                    break;
                case 2:
                    break;

                case 3:

                    break;

                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;

                case 7:

                    break;

                case 8:

                    break;
                case 9:

                    break;

                default:
                    throw new Exception("Some error...");
            }

            return View();
        }
        [HttpGet]
        public IActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(ViewProduct product)
        {
            Product p = new Product();
            if (product.Image != null)
            {
                byte[] byteArray = null;
                using (BinaryReader reader = new BinaryReader(product.Image.OpenReadStream()))
                {
                    byteArray = reader.ReadBytes((int)product.Image.Length);
                }
                p.Groups = new ProductGroups()
                {
                    DriedFruits = product.Groups.DriedFruits,
                    EasternMed = product.Groups.EasternMed,
                    Italian = product.Groups.Italian,
                    Nuts = product.Groups.Nuts,
                    Oils = product.Groups.Oils,
                    Sauces = product.Groups.Sauces,
                    Spice = product.Groups.Spice,
                    Sweets = product.Groups.Sweets,
                    Grocery = product.Groups.Grocery
                };
                p.Country = product.Country;
                p.Desctiption = product.Description;
                p.Name = product.Name;
                p.Price = product.Price;
                p.Image = byteArray;
                pc.Products.Add(p);
                await pc.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Tips()
        {
            ViewData["Message"] = "Tips";

            return View();
        }
        public IActionResult Action()
        {
            ViewData["Message"] = "Action";

            return View();
        }
        public IActionResult Services()
        {
            ViewData["Message"] = "Services";

            return View();
        }
        public IActionResult Cooperation()
        {
            ViewData["Message"] = "Cooperation";

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
