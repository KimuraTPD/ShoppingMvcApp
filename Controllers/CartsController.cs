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
        InvestoryControl ivc = new InvestoryControl();

        public CartsController(ShoppingMvcAppContext context)
        {
            _context = context;
            //User user = new User();
            
        }

        public IActionResult Index()
        {
            cartList = new List<Product>();
            if(HttpContext.Session.Get("object") != null){
                User user = new User(); 
                user = (User)user.ct(HttpContext.Session.Get("object"));
                ViewData["mail"] = user.mail;
                ViewData["pass"] =  user.password;
                ViewData["name"] = user.name;
                ViewData["tel"] = user.tel;
                ViewData["address"] = user.address;
                if(HttpContext.Session.Get("cartList") != null)
                {
                    cartList = (List<Product>)BytesToObject(HttpContext.Session.Get("cartList"));
                }
                
            }else{
                ViewData["Message"] = "ログインしてください。";  
               
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

        public async Task<IActionResult> OrderedPage(){
            User user = new User(); 
            if(HttpContext.Session.Get("object") != null){
                
                user = (User)user.ct(HttpContext.Session.Get("object"));
                ViewData["mail"] = user.mail;
                ViewData["pass"] =  user.password;
                ViewData["name"] = user.name;
                ViewData["tel"] = user.tel;
                ViewData["address"] = user.address;
            }else{
                 ViewData["Message"] = "ログインしてください。"; 
            }
            // Sessionにカートリストが存在する場合
            if(HttpContext.Session.Get("cartList") != null)
            {
                cartList = (List<Product>)BytesToObject(HttpContext.Session.Get("cartList"));

                //在庫管理の処理追加
                var icList = await _context.InvestoryControl.ToListAsync();
                foreach(var product in cartList){
                    foreach(var ic in icList){
                        if(product.productId == ic.productId){
                            if(ic.InvestoryAmount >= product.count && ic.InvestoryAmount - product.count >= 0){
                                ic.InvestoryAmount =ic.InvestoryAmount - product.count;
                                _context.Update(ic);
                                 await _context.SaveChangesAsync();
                            }else{
                                Console.WriteLine("在庫なし");
                                ViewData["investryMsg"] = "在庫がありません";
                                return View("../Carts/Index");
                            }
                        }
                    }
                }
                        await _context.SaveChangesAsync();

                PurchaseHistorysController phc = new PurchaseHistorysController(_context);
                await phc.CreatePurchaseHistory(user.userId, cartList);            
            }
            // Sessionにカートリストが存在しない、またはカートリストが空の場合
            if(HttpContext.Session.Get("cartList") == null || cartList.Count <= 0){
                ViewData["cartList"] = cartList;
                ViewData["EmptyCartMessage"] = "カートに商品を入れて、「注文確定」ボタンを押下してください。";
                return View("Index");
            }
            
            // ViewDataに購入が確定したcartListをセット
            ViewData["afterCartList"] = cartList;
            
            // カートを空にする
            cartList = new List<Product>();
            HttpContext.Session.Set("cartList", ObjectToBytes(cartList));   
            return View("../Carts/Ordered");
        }

         public async Task<IActionResult> DeleteCart(int? id)
        {
           
                User user = new User(); 
                user = (User)user.ct(HttpContext.Session.Get("object"));
                ViewData["mail"] = user.mail;
                ViewData["pass"] =  user.password;

                int id2 = (int)id;
    
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Product
                    .FirstOrDefaultAsync(m => m.productId == id);

                if (product == null)
                {
                    return NotFound();
                }

                // Sessionからカートを取得
                if(HttpContext.Session.Get("cartList") != null)
                {
                    Console.WriteLine("session get");
                    cartList = (List<Product>)BytesToObject(HttpContext.Session.Get("cartList"));
                }
                bool check = false;
                for(int i =0; i< cartList.Count; i++)
                {
                    // 既にカートに同じ商品が追加されている場合
                    if(cartList[i].productId== product.productId)
                    {
                        cartList.RemoveAt(i);
                        Console.WriteLine("cart");
                    }
                }

                foreach(Product p in cartList)
                {
                    p.showData();
                }
                Console.WriteLine("cartList.removeAt : " + cartList.Count);

                // カートリストをSessionにセット
                HttpContext.Session.Set("cartList",ObjectToBytes(cartList));

                // カートリストをViewDataにセット
                ViewData["cartList"] = cartList;


            return View("../Carts/index");
        }
         public static byte[] ObjectToBytes(Object ob)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, ob);
            return ms.ToArray();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}