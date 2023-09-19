using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;
using SchemaBuilder.models;

namespace PiggyBankMVC.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly PiggyContext _context;

        public ReviewsController(PiggyContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var piggyContext = _context.reviews.Include(r => r.Product).Include(r => r.Status).Include(r => r.User);
            return View(await piggyContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.reviews == null)
            {
                return NotFound();
            }

            var review = await _context.reviews
                .Include(r => r.Product)
                .Include(r => r.Status)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.products, "ProductId", "Color");
            ViewData["StatusId"] = new SelectList(_context.review_statuses, "ReviewStatusId", "Name");
            ViewData["UserId"] = new SelectList(_context.users, "UserId", "Email");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,Score,Message,CreatedAt,ProductId,StatusId,UserId")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.products, "ProductId", "Color", review.ProductId);
            ViewData["StatusId"] = new SelectList(_context.review_statuses, "ReviewStatusId", "Name", review.StatusId);
            ViewData["UserId"] = new SelectList(_context.users, "UserId", "Email", review.UserId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.reviews == null)
            {
                return NotFound();
            }

            var review = await _context.reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.products, "ProductId", "Color", review.ProductId);
            ViewData["StatusId"] = new SelectList(_context.review_statuses, "ReviewStatusId", "Name", review.StatusId);
            ViewData["UserId"] = new SelectList(_context.users, "UserId", "Email", review.UserId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,Score,Message,CreatedAt,ProductId,StatusId,UserId")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
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
            ViewData["ProductId"] = new SelectList(_context.products, "ProductId", "Color", review.ProductId);
            ViewData["StatusId"] = new SelectList(_context.review_statuses, "ReviewStatusId", "Name", review.StatusId);
            ViewData["UserId"] = new SelectList(_context.users, "UserId", "Email", review.UserId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.reviews == null)
            {
                return NotFound();
            }

            var review = await _context.reviews
                .Include(r => r.Product)
                .Include(r => r.Status)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.reviews == null)
            {
                return Problem("Entity set 'PiggyContext.reviews'  is null.");
            }
            var review = await _context.reviews.FindAsync(id);
            if (review != null)
            {
                _context.reviews.Remove(review);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
          return (_context.reviews?.Any(e => e.ReviewId == id)).GetValueOrDefault();
        }
    }
}
