using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;
using PiggyBankMVC.Migrations;
using PiggyBankMVC.Models;
using PiggyBankMVC.Models.ViewModels;
using System.Security.Claims;

namespace PiggyBankMVC.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly PiggyContext _context;
        private readonly string? _userId;
        private ShoppingCart? _cart;



        public ShoppingCartsController(PiggyContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _cart = ShoppingCart.CreateOrFind(_context, _userId);
        }



        // GET: ShoppingCarts
        public IActionResult Index(string lastUrl = "/")
        {
            _cart.GetItems();

            var vm = new ShoppingCartViewModel
            {
                LastUrl = lastUrl,
                ShoppingCart = _cart,
                TotalPrice = _cart.GetTotalPrice()
            };

            return View("Index", vm);
        }

        public async Task<IActionResult> Add(int productId, int quantity, string lastUrl = null)
        {
            Product? p = await _context.Products.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (p == null)
                return NotFound();

            lastUrl = lastUrl.Replace("%2F", "/");

            bool ok = _cart.Add(p, quantity);
            return Index(lastUrl);
        }

        public async Task<IActionResult> Remove(int productId)
        {
            Product? p = await _context.Products.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (p == null)
                return NotFound();

            _cart.Remove(p);

            return RedirectToAction("Index");
        }

        public IActionResult Back(string returnUrl = "/")
        {
            return Redirect(returnUrl);
        }

        private bool ShoppingCartExists(int id)
        {
          return (_context.ShoppingCarts?.Any(e => e.CartId == id)).GetValueOrDefault();
        }
    }
}
