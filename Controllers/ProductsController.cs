using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingMvcApp.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;

namespace ShoppingMvcApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShoppingMvcAppContext _context;

        public List<Product> cartList = new List<Product>();

        public ProductsController(ShoppingMvcAppContext context)
        {
            _context = context;

          
       //string a = ViewData["mail"].ToSting;
        
            // User user = new User(mail,pass);
            // var db_mail  = _context.User.Where(user => user.mail == str_mail).ToArray();
            // var db_pass  = _context.User.Where(user => user.password == str_pass).ToArray();

            //ユーザーIDとパスワードから電話番号/住所/名前を検索　保留
            //  var db_name  = _context.User.Where(user => user.mail == str_mail).ToArray();
            //  var db_tel  = _context.User.Where(user => user.mail == str_mail).ToArray();
            //  var db_address  = _context.User.Where((user => user.address == str_mail).ToArray();

            
            // HttpContext.Session.Set("object", user.ObjectToBytes(user));

            //  user = (User)user.ct(HttpContext.Session.Get("object"));
            //  ViewData["mail"] = user.mail;
            //  ViewData["pass"] =  user.password;
            //   Console.WriteLine("メアド" +user.mail);
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("Index：開始");
            User user = new User(); 
            if(HttpContext.Session.Get("object") != null){
                user = (User)user.ct(HttpContext.Session.Get("object"));
                ViewData["mail"] = user.mail;
                ViewData["pass"] =  user.password;
            }
            //  ViewData["mail"] = user.mail;
            //  ViewData["pass"] =  user.password;
            return View(await _context.Product.ToListAsync());
        }

        // カートに入れる
        public async Task<IActionResult> AddCart(int? id, int count)
        {
            if(HttpContext.Session.Get("object") != null){
                User user = new User(); 
                user = (User)user.ct(HttpContext.Session.Get("object"));
                ViewData["mail"] = user.mail;
                ViewData["pass"] =  user.password;
                Console.WriteLine("count = " + count);

                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Product
                    .FirstOrDefaultAsync(m => m.productId == id);
                product.count = count;
                if (product == null)
                {
                    return NotFound();
                }

                // Sessionからカートを取得
                if(HttpContext.Session.Get("cartList") != null)
                {
                    cartList = (List<Product>)BytesToObject(HttpContext.Session.Get("cartList"));
                }
                bool check = false;
                foreach(var item in cartList)
                {
                    // 既にカートに同じ商品が追加されている場合
                    if(item.productId == product.productId)
                    {
                        check = true;
                        // カート内の商品のカウントに加算
                        item.count += product.count;
                    }
                }
                // カート内に同じ商品が追加されていない場合
                if(!check)
                {
                    // カートリストに追加
                    cartList.Add(product);
                }

                foreach(Product p in cartList)
                {
                    p.showData();
                }

                // カートリストをSessionにセット
                HttpContext.Session.Set("cartList",ObjectToBytes(cartList));

                // カートリストをViewDataにセット
                ViewData["cartList"] = cartList;

                ViewData["Message"] = "カートに追加しました。";
                    ViewData["cartList"] = cartList;
            }else{
                ViewData["Message"] = "ログインしてください。";     
            }
            return View("../Products/index", await _context.Product.ToListAsync());
        }
       

        public static byte[] ObjectToBytes(Object ob)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, ob);
            return ms.ToArray();
        }

        public static Object BytesToObject(byte[] arr)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            ms.Write(arr, 0, arr.Length);
            ms.Seek(0, SeekOrigin.Begin);
            return (Object)bf.Deserialize(ms);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("productId,productName,price,create_date,image_url")] Product product)
        {
            product.count = 0;
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("productId,productName,price,create_date,image_url")] Product product)
        {
            if (id != product.productId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.productId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.productId == id);
        }
    }
}
