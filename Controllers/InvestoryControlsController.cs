using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingMvcApp.Models;

namespace ShoppingMvcApp.Controllers
{
    public class InvestoryControlsController : Controller
    {
        private readonly ShoppingMvcAppContext _context;

        public InvestoryControlsController(ShoppingMvcAppContext context)
        {
            _context = context;
        }

        // GET: InvestoryControls
        public async Task<IActionResult> Index()
        {
            return View(await _context.InvestoryControl.ToListAsync());
        }

        // GET: InvestoryControls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investoryControl = await _context.InvestoryControl
                .FirstOrDefaultAsync(m => m.productId == id);
            if (investoryControl == null)
            {
                return NotFound();
            }

            return View(investoryControl);
        }

        // GET: InvestoryControls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InvestoryControls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("productId,InvestoryAmount")] InvestoryControl investoryControl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(investoryControl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(investoryControl);
        }

        // GET: InvestoryControls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investoryControl = await _context.InvestoryControl.FindAsync(id);
            if (investoryControl == null)
            {
                return NotFound();
            }
            return View(investoryControl);
        }

        // POST: InvestoryControls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("productId,InvestoryAmount")] InvestoryControl investoryControl)
        {
            if (id != investoryControl.productId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investoryControl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestoryControlExists(investoryControl.productId))
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
            return View(investoryControl);
        }

        // GET: InvestoryControls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investoryControl = await _context.InvestoryControl
                .FirstOrDefaultAsync(m => m.productId == id);
            if (investoryControl == null)
            {
                return NotFound();
            }

            return View(investoryControl);
        }

        // POST: InvestoryControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var investoryControl = await _context.InvestoryControl.FindAsync(id);
            _context.InvestoryControl.Remove(investoryControl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestoryControlExists(int id)
        {
            return _context.InvestoryControl.Any(e => e.productId == id);
        }
    }
}
