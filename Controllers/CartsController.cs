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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// using Microsoft.AspNetCore.Authorization;

namespace ShoppingMvcApp.Controllers
{
    public class CartsController : Controller
    {
        private ShoppingMvcAppContext _context;

        public List<Product> cartList = new List<Product>();

        public CartsController(ShoppingMvcAppContext context)
        {
            _context = context;
            //User user = new User();
        }

        public IActionResult Index()
        {
            User user = new User(); 
            user = (User)user.ct(HttpContext.Session.Get("object"));
            ViewData["mail"] = user.mail;
            ViewData["pass"] =  user.password;
            if(HttpContext.Session.Get("cartList") != null)
            {
                cartList = (List<Product>)BytesToObject(HttpContext.Session.Get("cartList"));
            }
            ViewData["cartList"] = cartList;
            return View();
        }

        public static Object BytesToObject(byte[] arr)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            ms.Write(arr, 0, arr.Length);
            ms.Seek(0, SeekOrigin.Begin);
            return (Object)bf.Deserialize(ms);
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
            User user = new User(); 
            user = (User)user.ct(HttpContext.Session.Get("object"));
            ViewData["mail"] = user.mail;
            ViewData["pass"] =  user.password;
            return View("../Carts/Ordered");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}