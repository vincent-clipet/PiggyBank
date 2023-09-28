using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;
using PiggyBankMVC.Models;

namespace PiggyBankMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly PiggyContext _context;

        public ProductsController(PiggyContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            bool isCustomerOrGuest = User.IsInRole("Customer") || !User.Identity.IsAuthenticated;

            var orders = isCustomerOrGuest ?
                _context.Products.Include(p => p.Manufacturer).Where(p => p.IsActive == true)
                :
                _context.Products.Include(p => p.Manufacturer);

            return View(orders);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            bool isCustomerOrGuest = User.IsInRole("Customer") || !User.Identity.IsAuthenticated;

            if (id == null || _context.Products == null) return NotFound();

            Product? product = await _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null) return NotFound();
            if (!product.IsActive && isCustomerOrGuest) return Forbid();

            List<Review> reviews = product.Reviews.Take(3).ToList();
            ViewData["Reviews"] = reviews;

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ImageUrl,Description,Height,Width,Length,Weight,Capacity,Color,Price,IsActive,ManufacturerId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "Name", product.ManufacturerId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "Name", product.ManufacturerId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ImageUrl,Description,Height,Width,Length,Weight,Capacity,Color,Price,IsActive,ManufacturerId")] Product product)
        {
            if (id != product.ProductId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "Name", product.ManufacturerId);
            return View(product);
        }

        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Products == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Manufacturer)
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Products == null)
        //    {
        //        return Problem("Entity set 'PiggyContext.Products'  is null.");
        //    }
        //    var product = await _context.Products.FindAsync(id);
        //    if (product != null)
        //    {
        //        _context.Products.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
