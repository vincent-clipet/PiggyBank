using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;
using PiggyBankMVC.Models.ViewModels;

namespace PiggyBankMVC.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly PiggyContext _context;

        public OrderDetailsController(PiggyContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var piggyContext = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            return View(await piggyContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderDetails == null) return NotFound();

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);

            if (orderDetail == null) return NotFound();

            var vm = new OrderDetailsViewModel
            {
                OrderDetailId = orderDetail.OrderId,
                Order = orderDetail.Order,
                Product = orderDetail.Product,
                Price = orderDetail.Price,
                Quantity = orderDetail.Quantity
            };

            return View(vm);
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}
