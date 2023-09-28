using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.DataAccessLayer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiggyBankMVC.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public List<ShoppingCartItem> Items { get; } = new List<ShoppingCartItem>();

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }



        private PiggyContext _context;



        public ShoppingCart(PiggyContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Adds a new Product to this ShoppingCart
        /// </summary>
        /// <returns>True if the item was added to cart. False if the quantity was invalid</returns>
        public bool Add(Product p, int quantity)
        {
            if (quantity <= 0) return false;

            // Check if this Product was already added to this ShoppingCart.
            ShoppingCartItem? shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(s => s.ProductId == p.ProductId && s.CartId == this.CartId);

            // Item not yet present in the ShoppingCart
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    Quantity = quantity,
                    CartId = this.CartId,
                    ProductId = p.ProductId
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            // Item already in the ShoppingCart
            else
            {
                shoppingCartItem.Quantity += quantity;
            }

            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Remove a Product from this ShoppingCart
        /// </summary>
        /// <returns>True if the item was removed from cart. False if it didn't exist</returns>
        public bool Remove(Product p)
        {
            ShoppingCartItem? shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(s => s.ProductId == p.ProductId && s.CartId == this.CartId);

            if (shoppingCartItem != null)
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public List<ShoppingCartItem> GetItems()
        {
            if (this.Items != null)
                return this.Items;
            else
                return _context.ShoppingCartItems.Where(cart => cart.CartId == this.CartId).Include(s => s.Product).ToList();
        }

        public void Wipe()
        {
            var cartItemsToWipe = _context.ShoppingCartItems.Where(cart => cart.CartId == this.CartId);
            _context.ShoppingCartItems.RemoveRange(cartItemsToWipe);
            _context.SaveChanges();
        }

        public decimal GetTotalPrice()
        {
            return _context.ShoppingCartItems.Where(cart => cart.CartId == this.CartId).Select(item => item.Product.Price * item.Quantity).Sum();
        }

        public static ShoppingCart CreateOrFind(PiggyContext _context, string userId)
        {
            ShoppingCart? cart = _context.ShoppingCarts.Where(s => s.UserId == userId).Include(s => s.Items).ThenInclude(s => s.Product).FirstOrDefault();
            if (cart == null)
            {
                cart = new ShoppingCart(_context)
                {
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };
                _context.ShoppingCarts.Add(cart);
                _context.SaveChanges();
                return cart;
            }
            else
            {
                return cart;
            }
        }
    }
}
