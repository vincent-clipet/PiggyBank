using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;
using PiggyBankMVC.Models;

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
        [Authorize(Roles = "Admin,Moderator")] // TODO: add exception for current user
        public async Task<IActionResult> Index()
        {
            var piggyContext = _context.Reviews.Include(r => r.Product).Include(r => r.User);
            return View(await piggyContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        [Authorize(Roles = "Admin,Moderator")] // TODO: add exception for current user
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        [Authorize(Roles = "Customer")] // TODO: add exception for current user
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Color");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")] // TODO: add exception for current user
        public async Task<IActionResult> Create([Bind("ReviewId,Score,Message,CreatedAt,ProductId,UserId,ReviewStatus")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Color", review.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        [Authorize(Roles = "Admin,Moderator")] // TODO: add exception for current user
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Color", review.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Moderator")] // TODO: add exception for current user
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,Score,Message,CreatedAt,ProductId,UserId,ReviewStatus")] Review review)
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Color", review.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);
            return View(review);
        }

        private bool ReviewExists(int id)
        {
          return (_context.Reviews?.Any(e => e.ReviewId == id)).GetValueOrDefault();
        }
    }
}
