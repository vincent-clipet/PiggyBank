using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;
using PiggyBankMVC.Models;
using PiggyBankMVC.Models.Enums;
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
        [Authorize(Roles = "Admin,Assist")] // TODO: add exception for current user
        public async Task<IActionResult> Index()
        {
            var piggyContext = _context.Orders.Include(o => o.Address).Include(o => o.User);
            return View(await piggyContext.ToListAsync());
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Admin,Assist")] // TODO: add exception for current user
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Address)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            var orderDetails = _context.OrderDetails
                .Include(o => o.Product)
                .Where(m => m.OrderId == id).ToList();

            var vm = new OrderViewModel
            {
                Order = order,
                Details = orderDetails,
            };

            Console.WriteLine(vm.Details);

            return View(vm);
        }

        //// GET: Orders/Create
        //public IActionResult Create()
        //{
        //    ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "City");
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        //    return View();
        //}

        //// POST: Orders/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("OrderId,CreatedAt,UserId,AddressId,OrderStatus")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "City", order.AddressId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
        //    return View(order);
        //}

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin,Assist")] // TODO: add exception for current user
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "City", order.AddressId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            ViewData["OrderStatus"] = new SelectList(EnumHelper.GetSelectList(typeof(EnumOrderStatus)), order.OrderStatus);
            var tmp = ViewData;

            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assist")]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CreatedAt,UserId,AddressId,OrderStatus")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "City", order.AddressId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
