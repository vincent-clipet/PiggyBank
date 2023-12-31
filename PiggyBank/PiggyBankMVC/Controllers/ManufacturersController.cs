﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;
using PiggyBankMVC.Models;

namespace PiggyBankMVC.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly PiggyContext _context;

        public ManufacturersController(PiggyContext context)
        {
            _context = context;
        }

        // GET: Manufacturers
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var piggyContext = _context.Manufacturers.Include(m => m.Address);
            return View(await piggyContext.ToListAsync());
        }

        // GET: Manufacturers/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Manufacturers == null) return NotFound();

            var manufacturer = await _context.Manufacturers
                .Include(m => m.Address)
                .FirstOrDefaultAsync(m => m.ManufacturerId == id);

            if (manufacturer == null) return NotFound();

            return View(manufacturer);
        }

        // GET: Manufacturers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "City");
            return View();
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ManufacturerId,Name,AddressId")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manufacturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "City", manufacturer.AddressId);
            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Manufacturers == null) return NotFound();

            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null) return NotFound();

            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "City", manufacturer.AddressId);
            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ManufacturerId,Name,AddressId")] Manufacturer manufacturer)
        {
            if (id != manufacturer.ManufacturerId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manufacturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufacturerExists(manufacturer.ManufacturerId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "City", manufacturer.AddressId);
            return View(manufacturer);
        }

        private bool ManufacturerExists(int id)
        {
            return (_context.Manufacturers?.Any(e => e.ManufacturerId == id)).GetValueOrDefault();
        }
    }
}
