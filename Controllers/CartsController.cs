using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingMvcApp.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;

// using Microsoft.AspNetCore.Authorization;

namespace ShoppingMvcApp.Controllers
{
    public class CartsController : Controller
    {
        private ShoppingMvcAppContext _context;

        public CartsController(ShoppingMvcAppContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        // [Authorize]
        public IActionResult Form()
        {
            return View();
        }

 //カートサイトに表示用
        // public void DisplayCartToProduct(int productId, string productName, int price){
        //     // Sessionからカートを取得
        //     if(HttpContext.Session.Get("cartList") != null)
        //     {
        //         cartList = (List<Product>)BytesToObject(HttpContext.Session.Get("cartList"));
        //     }
        //   //  Console.WriteLine("id = " + productId + ", name = " + productName + ", price = " + price);

        //     Product product = new Product(productId, productName, price);
        //     foreach(Product p in cartList)
        //     {
        //         p.showData();
        //     }
        // }
        public IActionResult OrderedPage(){
            return View("../Carts/Ordered");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
