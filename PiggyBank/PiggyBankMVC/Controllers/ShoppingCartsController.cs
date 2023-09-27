using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;
using PiggyBankMVC.Models;

namespace PiggyBankMVC.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly PiggyContext _context;

        public ShoppingCartsController(PiggyContext context)
        {
            _context = context;
        }

        // GET: ShoppingCarts
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.ShoppingCarts != null ? 
                          View(await _context.ShoppingCarts.ToListAsync()) :
                          Problem("Entity set 'PiggyContext.ShoppingCarts'  is null.");
        }

        // GET: ShoppingCarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShoppingCarts == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }


        // POST: ShoppingCarts/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CartId,CreatedAt")] ShoppingCart shoppingCart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(shoppingCart);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(shoppingCart);
        //}

        // GET: ShoppingCarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShoppingCarts == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,CreatedAt")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.CartId))
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
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.ShoppingCarts == null)
        //    {
        //        return NotFound();
        //    }

        //    var shoppingCart = await _context.ShoppingCarts
        //        .FirstOrDefaultAsync(m => m.CartId == id);
        //    if (shoppingCart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(shoppingCart);
        //}

        // POST: ShoppingCarts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.ShoppingCarts == null)
        //    {
        //        return Problem("Entity set 'PiggyContext.ShoppingCarts'  is null.");
        //    }
        //    var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
        //    if (shoppingCart != null)
        //    {
        //        _context.ShoppingCarts.Remove(shoppingCart);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ShoppingCartExists(int id)
        {
          return (_context.ShoppingCarts?.Any(e => e.CartId == id)).GetValueOrDefault();
        }
    }
}
