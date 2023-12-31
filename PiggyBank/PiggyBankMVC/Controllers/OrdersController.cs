﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;
using PiggyBankMVC.Models;
using PiggyBankMVC.Models.ViewModels;

namespace PiggyBankMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly PiggyContext _context;

        public OrdersController(PiggyContext context)
        {
            _context = context;
        }

        // GET: Orders
        [Authorize(Roles = "Admin,Assist,Customer")] // TODO: add exception for current user
        public async Task<IActionResult> Index()
        {
            bool isCustomer = User.IsInRole("Customer");
            string? userId = ApplicationUser.GetUserId(User);

            if (userId == null) return NotFound();

            var orders = isCustomer ?
                _context.Orders.Include(o => o.Address).Include(o => o.User).OrderByDescending(o => o.CreatedAt).Where(o => o.UserId == userId)
                :
                _context.Orders.Include(o => o.Address).Include(o => o.User).OrderByDescending(o => o.CreatedAt);

            return View(orders);
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Admin,Assist,Customer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null) return NotFound();

            Order? order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound();

            bool isCustomer = User.IsInRole("Customer");

            // This order wasn't created by this user, return
            if (isCustomer && order.UserId != ApplicationUser.GetUserId(User)) return Forbid();

            order = await _context.Orders
                        .Include(o => o.Address)
                        .Include(o => o.User)
                        .FirstOrDefaultAsync(m => m.OrderId == id);

            var orderDetails = _context.OrderDetails
                .Include(o => o.Product)
                .Where(m => m.OrderId == id).ToList();

            return View(new OrderViewModel(order, orderDetails));
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin,Assist")] // TODO: add exception for current user
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Address)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null) return NotFound();

            var orderDetails = _context.OrderDetails
                .Include(o => o.Product)
                .Where(m => m.OrderId == id).ToList();

            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "City", order.AddressId);
            ViewData["UserId"] = new SelectList(_context.Users, "Email", "Email", order.UserId);

            return View(new OrderViewModel(order, orderDetails));
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assist")]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CreatedAt,UserId,AddressId,OrderStatus")] Order order)
        {
            if (id != order.OrderId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var orderDetails = _context.OrderDetails
                .Include(o => o.Product)
                .Where(m => m.OrderId == id).ToList();

            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "City", order.AddressId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", order.UserId);

            return View(new OrderViewModel(order, orderDetails));
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
