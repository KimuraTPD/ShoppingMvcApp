using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingMvcApp.Models;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;

namespace ShoppingMvcApp.Controllers
{
    public class PurchaseHistorysController : Controller
    {
        private readonly ShoppingMvcAppContext _context;

        public PurchaseHistorysController(ShoppingMvcAppContext context)
        {
            _context = context;
        }

        public async Task CreatePurchaseHistory(int userId, List<Product> productList)
        {
            Console.WriteLine("CreateRecord：開始");
            // 購入履歴テーブルから購入確定を行ったユーザーデータを取得
            var histories = await _context.PurchaseHistory.Where(p => p.userId == userId).ToListAsync();
            
            // 最新の明細番号を取得
            int detailsId = 0;
            foreach(var ph in histories)
            {
                if(detailsId < ph.detailsId)
                {
                    detailsId = ph.detailsId;
                }
            }
            // 最新の明細番号に1を加算
            detailsId++;
            // 各商品毎に購入履歴インスタンスを生成し、DBに追加
            foreach(var p in productList)
            {
                PurchaseHistory ph = new PurchaseHistory();
                ph.detailsId = detailsId;
                ph.userId = userId;
                ph.productId = p.productId;
                ph.count = p.count;
                ph.purchaseDate = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                ph.showData();
                _context.Add(ph);
            }
            await _context.SaveChangesAsync();
            Console.WriteLine("購入履歴明細テーブルに追加しました。");
        }

        // GET: PurchaseHistorys
        public async Task<IActionResult> Index()
        {
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
            var phList = await _context.PurchaseHistory.Where(ph => ph.userId == user.userId).OrderByDescending(ph => ph.detailsId).ToListAsync();
            var list = await _context.PurchaseHistory.Where(ph1 => ph1.userId == user.userId).OrderByDescending(ph => ph.detailsId)
            .Join( _context.Product,
            ph => ph.productId,
            p => p.productId,
            (ph, p) => new JoinTables {
                product = p, purchaseHistory = ph
            }
            ).ToListAsync();
            ViewData["historyList"] = list;
            return View(list);
        }

        // GET: PurchaseHistorys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseHistory = await _context.PurchaseHistory
                .FirstOrDefaultAsync(m => m.PurchaseHistoryId == id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }

            return View(purchaseHistory);
        }

        // GET: PurchaseHistorys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurchaseHistorys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseHistoryId,detailsId,userId,productId,count,purchaseDate")] PurchaseHistory purchaseHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseHistory);
        }

        // GET: PurchaseHistorys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseHistory = await _context.PurchaseHistory.FindAsync(id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }
            return View(purchaseHistory);
        }

        // POST: PurchaseHistorys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseHistoryId,detailsId,userId,productId,count,purchaseDate")] PurchaseHistory purchaseHistory)
        {
            if (id != purchaseHistory.PurchaseHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseHistoryExists(purchaseHistory.PurchaseHistoryId))
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
            return View(purchaseHistory);
        }

        // GET: PurchaseHistorys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseHistory = await _context.PurchaseHistory
                .FirstOrDefaultAsync(m => m.PurchaseHistoryId == id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }

            return View(purchaseHistory);
        }

        // POST: PurchaseHistorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseHistory = await _context.PurchaseHistory.FindAsync(id);
            _context.PurchaseHistory.Remove(purchaseHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseHistoryExists(int id)
        {
            return _context.PurchaseHistory.Any(e => e.PurchaseHistoryId == id);
        }
    }
}
