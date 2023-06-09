using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;

namespace ShoppingMvcApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShoppingMvcAppContext _context;

        private List<Product> cartList = new List<Product>();

        public ProductsController(ShoppingMvcAppContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("Index：開始");
            return View(await _context.Product.ToListAsync());
        }

        // [HttpPost]
        // public async Task<IActionResult> AddCart(int productId, string productName, int price) {
        //     string productNameFromRequestForm = Request.Form["productName"];

        //     Console.WriteLine("nameFromForm" )

        //     // Sessionからカートを取得
        //     if(HttpContext.Session.Get("cartList") != null)
        //     {
        //         cartList = (List<Product>)BytesToObject(HttpContext.Session.Get("cartList"));
        //     }
        //     Console.WriteLine("id = " + productId + ", name = " + productName + ", price = " + price);

        //     Product product = new Product(productId, productName, price);

        //     // カートリストに追加
        //     cartList.Add(product);

        //     foreach(Product p in cartList)
        //     {
        //         p.showData();
        //     }

        //     HttpContext.Session.Set("cartList",ObjectToBytes(cartList));

        //     return View("../Products/index", await _context.Product.ToListAsync());
        // }

        
        public async Task<IActionResult> AddCart(int? id)
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

            // Sessionからカートを取得
            if(HttpContext.Session.Get("cartList") != null)
            {
                cartList = (List<Product>)BytesToObject(HttpContext.Session.Get("cartList"));
            }

            // カートリストに追加
            cartList.Add(product);

            foreach(Product p in cartList)
            {
                p.showData();
            }

            HttpContext.Session.Set("cartList",ObjectToBytes(cartList));

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
