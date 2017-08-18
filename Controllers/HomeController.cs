using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Al_Baraka.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
            List<List<Product>> AllGroups = new List<List<Product>>();

            List<Product> group0 = new List<Product>();
            group0 = (from g in pc.Products.Include(g => g.Groups).ToList() where g.Groups.DriedFruits == true select g).ToList();
            AllGroups.Add(group0);

            List<Product> group1 = new List<Product>();
            group1 = (from g in pc.Products.Include(g => g.Groups).ToList() where g.Groups.EasternMed == true select g).ToList();
            AllGroups.Add(group1);

            List<Product> group2 = new List<Product>();
            group2 = (from g in pc.Products.Include(g => g.Groups).ToList() where g.Groups.Grocery == true select g).ToList();
            AllGroups.Add(group2);

            List<Product> group3 = new List<Product>();
            group3 = (from g in pc.Products.Include(g => g.Groups).ToList() where g.Groups.Italian == true select g).ToList();
            AllGroups.Add(group3);

            List<Product> group4 = new List<Product>();
            group4 = (from g in pc.Products.Include(g => g.Groups).ToList() where g.Groups.Nuts == true select g).ToList();
            AllGroups.Add(group4);

            List<Product> group5 = new List<Product>();
            group5 = (from g in pc.Products.Include(g => g.Groups).ToList() where g.Groups.Oils == true select g).ToList();
            AllGroups.Add(group5);

            List<Product> group6 = new List<Product>();
            group6 = (from g in pc.Products.Include(g => g.Groups).ToList() where g.Groups.Sauces == true select g).ToList();
            AllGroups.Add(group6);

            List<Product> group7 = new List<Product>();
            group7 = (from g in pc.Products.Include(g => g.Groups).ToList() where g.Groups.Spice == true select g).ToList();
            AllGroups.Add(group7);

            List<Product> group8 = new List<Product>();
            group8 = (from g in pc.Products.Include(g => g.Groups).ToList() where g.Groups.Sweets == true select g).ToList();
            AllGroups.Add(group8);

            return View(AllGroups);
        }
        public IActionResult ShowOneGroup(int idgroup)
        {
            List<Product> result = null;
            switch (idgroup)
            {
                case 1:
                    View(pc.Products.ToList());
                    break;
                case 2:
                    result = (from p in pc.Products where p.Groups.Sweets == true select p).ToList();
                    View(result);
                    break;

                case 3:
                    result = (from p in pc.Products where p.Groups.DriedFruits == true select p).ToList();
                    View(result);
                    break;

                case 4:
                    result = (from p in pc.Products where p.Groups.Spice == true select p).ToList();
                    View(result);
                    break;

                case 5:
                    result = (from p in pc.Products where p.Groups.Nuts == true select p).ToList();
                    View(result);
                    break;

                case 6:
                    result = (from p in pc.Products where p.Groups.Oils == true select p).ToList();
                    View(result);
                    break;

                case 7:
                    result = (from p in pc.Products where p.Groups.Sauces == true select p).ToList();
                    View(result);
                    break;

                case 8:
                    result = (from p in pc.Products where p.Groups.Italian == true select p).ToList();
                    View(result);
                    break;

                case 9:
                    result = (from p in pc.Products where p.Groups.EasternMed == true select p).ToList();
                    View(result);
                    break;
                case 10:
                    result = (from p in pc.Products where p.Groups.Grocery == true select p).ToList();
                    View(result);
                    break;

                default:
                    throw new Exception("Fail of group product...");
            }
            return View(null);
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddNew()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNew(ViewProduct product)
        {
            Product p = new Product();
            byte[] byteArray = null;
            if (product.Image != null)
            {
                using (BinaryReader reader = new BinaryReader(product.Image.OpenReadStream()))
                {
                    byteArray = reader.ReadBytes((int)product.Image.Length);
                }
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
            p.Description = product.Description;
            p.Name = product.Name;
            p.Price = product.Price;
            p.Image = byteArray;
            p.Measure = product.Measure;

            pc.Products.Add(p);
            //pc.SaveChanges();

            //p.Groups.ProductId = p.Id;
            pc.ProductGroups.Add(p.Groups);
            pc.SaveChanges();

            return RedirectToAction("listofproducts", "Home");
        }
        public IActionResult Tips()
        {
            ViewData["Message"] = "Tips";

            return View();
        }
        public IActionResult Grocery()
        {
            ViewData["Message"] = "Grocery";

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

        public IActionResult Details(int IdProduct)
        {
            ViewData["Message"] = "Details.";
            Product prod = pc.Products.First((p) => p.Id == IdProduct);
            if (prod == null)
                throw new Exception("Product by 'IdProduct' was not found!");
            return View(prod);
        }
        [Authorize]
        [HttpGet]
        public IActionResult ListOfProducts()
        {
            return View(pc.Products.Include(g=>g.Groups).ToList());
        }
        [Authorize]
        [HttpPost]
        public IActionResult ListOfProducts(string searchPattern)
        {
            if (searchPattern == null)
                searchPattern = "";
            List<Product> result = (from p in pc.Products where p.Name.ToLower().Contains(searchPattern.ToLower()) || p.Id.ToString() == searchPattern select p).Include(p=>p.Groups).ToList();
            return View(result);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int idproduct)
        {
            Product Editingproduct = pc.Products.Include((p) => p.Groups).Where(p => p.Id == idproduct).FirstOrDefault();
            if (Editingproduct != null)
                return View(Editingproduct);
            throw new Exception("Not find product by id");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(ViewProduct EditingProduct)
        {
            Product product = pc.Products.Include(p => p.Groups).SingleOrDefault((s) => s.Id == EditingProduct.Id);

            if (product != null)
            {
                byte[] byteArray = null;
                if (EditingProduct.Image != null)
                {
                    using (BinaryReader reader = new BinaryReader(EditingProduct.Image.OpenReadStream()))
                    {
                        byteArray = reader.ReadBytes((int)EditingProduct.Image.Length);
                    }
                    product.Image = byteArray;
                }
                product.Name = EditingProduct.Name;
                product.Description = EditingProduct.Description;
                product.Country = EditingProduct.Country;
                product.Measure = EditingProduct.Measure;
                product.Price = EditingProduct.Price;
                product.Groups.DriedFruits = EditingProduct.Groups.DriedFruits;
                product.Groups.EasternMed = EditingProduct.Groups.EasternMed;
                product.Groups.Grocery = EditingProduct.Groups.Grocery;
                product.Groups.Italian = EditingProduct.Groups.Italian;
                product.Groups.Nuts = EditingProduct.Groups.Nuts;
                product.Groups.Oils = EditingProduct.Groups.Oils;
                product.Groups.Sauces = EditingProduct.Groups.Sauces;
                product.Groups.Spice = EditingProduct.Groups.Spice;
                product.Groups.Sweets = EditingProduct.Groups.Sweets;

                TryUpdateModelAsync<Product>(product);
                pc.SaveChanges();
            }
            return RedirectToAction("listofproducts", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int idproduct)
        {
            return View(idproduct);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(int idproduct, string name)
        {
            Product product = pc.Products.Include(p => p.Groups).FirstOrDefault((p) => p.Id == idproduct);
            if (product != null)
            {
                pc.Products.Remove(product);
                pc.SaveChanges();
                return RedirectToAction("listofproducts", "Home");
            }
            throw new Exception("Not find product by id");
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
