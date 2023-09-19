﻿using System;
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
    public class ManufacturersController : Controller
    {
        private readonly PiggyContext _context;

        public ManufacturersController(PiggyContext context)
        {
            _context = context;
        }

        // GET: Manufacturers
        public async Task<IActionResult> Index()
        {
            var piggyContext = _context.manufacturers.Include(m => m.Address);
            return View(await piggyContext.ToListAsync());
        }

        // GET: Manufacturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.manufacturers == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.manufacturers
                .Include(m => m.Address)
                .FirstOrDefaultAsync(m => m.ManufacturerId == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // GET: Manufacturers/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.addresses, "AddressId", "City");
            return View();
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManufacturerId,Name,AddressId")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manufacturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.addresses, "AddressId", "City", manufacturer.AddressId);
            return View(manufacturer);
        }

        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.manufacturers == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.addresses, "AddressId", "City", manufacturer.AddressId);
            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManufacturerId,Name,AddressId")] Manufacturer manufacturer)
        {
            if (id != manufacturer.ManufacturerId)
            {
                return NotFound();
            }

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
            ViewData["AddressId"] = new SelectList(_context.addresses, "AddressId", "City", manufacturer.AddressId);
            return View(manufacturer);
        }

        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.manufacturers == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.manufacturers
                .Include(m => m.Address)
                .FirstOrDefaultAsync(m => m.ManufacturerId == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.manufacturers == null)
            {
                return Problem("Entity set 'PiggyContext.manufacturers'  is null.");
            }
            var manufacturer = await _context.manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                _context.manufacturers.Remove(manufacturer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManufacturerExists(int id)
        {
          return (_context.manufacturers?.Any(e => e.ManufacturerId == id)).GetValueOrDefault();
        }
    }
}