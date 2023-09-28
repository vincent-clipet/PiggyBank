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
    public class AddressesController : Controller
    {
        private readonly PiggyContext _context;

        public AddressesController(PiggyContext context)
        {
            _context = context;
        }

        // GET: Addresses
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.Addresses != null ? 
                          View(await _context.Addresses.ToListAsync()) :
                          Problem("Entity set 'PiggyContext.Addresses'  is null.");
        }

        // GET: Addresses/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Addresses == null) return NotFound();

            var address = await _context.Addresses
                .FirstOrDefaultAsync(m => m.AddressId == id);

            if (address == null) return NotFound();

            return View(address);
        }

        // GET: Addresses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("AddressId,Number,Street,City,Zip")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Addresses/Edit/5
        [Authorize(Roles = "Admin")] // TODO: exception for current User
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Addresses == null) return NotFound();

            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return NotFound();

            return View(address);
        }

        // POST: Addresses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // TODO: exception for current User
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,Number,Street,City,Zip")] Address address)
        {
            if (id != address.AddressId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        private bool AddressExists(int id)
        {
          return (_context.Addresses?.Any(e => e.AddressId == id)).GetValueOrDefault();
        }
    }
}
