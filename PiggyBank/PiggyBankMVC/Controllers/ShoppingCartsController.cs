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
using System.Web.Helpers;

namespace PiggyBankMVC.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly PiggyContext _context;
        private readonly string? _userId;



        public ShoppingCartsController(PiggyContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }



        // GET: ShoppingCarts
        public IActionResult Index(string lastUrl = "/")
        {
            ShoppingCart? cart = _context.ShoppingCarts.Where(s => s.UserId == _userId).Include(s => s.Items).ThenInclude(s => s.Product).FirstOrDefault();

            if (cart == null)
            {
                var vm = new ShoppingCartViewModel
                {
                    LastUrl = null,
                    ShoppingCart = null,
                    TotalPrice = 0,
                };
                return View("Index", vm);
            }
            else
            {
                var vm = new ShoppingCartViewModel
                {
                    LastUrl = lastUrl,
                    ShoppingCart = cart,
                    TotalPrice = cart.GetTotalPrice()
                };
                return View("Index", vm);
            }
        }

        public async Task<IActionResult> Add(int productId, int quantity, string lastUrl = null)
        {
            Product? p = await _context.Products.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (p == null)
                return NotFound();

            lastUrl = lastUrl.Replace("%2F", "/");

            ShoppingCart _cart = ShoppingCart.CreateOrFind(_context, _userId);

            bool ok = _cart.Add(p, quantity);
            return Index(lastUrl);
        }

        public async Task<IActionResult> Remove(int productId)
        {
            Product? p = await _context.Products.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (p == null)
                return NotFound();

            ShoppingCart _cart = ShoppingCart.CreateOrFind(_context, _userId);

            _cart.Remove(p);

            return RedirectToAction("Index");
        }

        public IActionResult Wipe()
        {
            ShoppingCart _cart = ShoppingCart.CreateOrFind(_context, _userId);

            _context.ShoppingCartItems.RemoveRange(_context.ShoppingCartItems.Where(i => i.CartId == _cart.CartId));
            _context.ShoppingCarts.Remove(_cart);
            _context.SaveChanges();
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
