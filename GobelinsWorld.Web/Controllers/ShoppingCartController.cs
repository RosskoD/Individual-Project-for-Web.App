namespace GobelinsWorld.Web.Controllers
{
    using Data;
    using Data.Models;
    using GobelinsWorld.Services.Admin;
    using GobelinsWorld.Services.ShoppingCart;
    using GobelinsWorld.Services.ShoppingCart.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.ShoppingCart;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly GobelinsWorldDbContext db;
        private readonly ICategoryService categories;
        private readonly IProducerService producers;
        private readonly UserManager<User> userManager;

        public ShoppingCartController(IShoppingCartService shoppingCartService, GobelinsWorldDbContext db, ICategoryService categories, IProducerService producers, UserManager<User> userManager)
        {
            this.shoppingCartService = shoppingCartService;
            this.db = db;
            this.categories = categories;
            this.producers = producers;
            this.userManager = userManager;
        }

        public IActionResult AddToCart(int id, [FromForm] int quantity)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartService.AddToCart(shoppingCartId, id, quantity);

            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveFromCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartService.RemoveFromCart(shoppingCartId, id);

            return RedirectToAction(nameof(Details));
        }

        public IActionResult Clear()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartService.Clear(shoppingCartId);

            return RedirectToAction(nameof(Details));
        }

        public IActionResult Update(int id, [FromForm] int quantity)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartService.GetItems(shoppingCartId);

            var itemsWithDetails = GetCartItems(items);

            this.shoppingCartService.EditQuantity(shoppingCartId, id, quantity);

            return RedirectToAction(nameof(Details));
        }

        public async Task<IActionResult> Details()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartService.GetItems(shoppingCartId);

            var itemsWithDetails = GetCartItems(items);

            var allCategories = await this.categories.All();
            var allProducers = await this.producers.Brands();

            return View(new CartItemListingViewModel
            {
                Products = itemsWithDetails,
                Categories = allCategories,
                Producers = allProducers
            });
        }

        [Authorize]
        public async Task<IActionResult> Finish()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartService.GetItems(shoppingCartId);

            var itemsWithDetails = GetCartItems(items);

            var order = new Order
            {
                UserId = this.userManager.GetUserId(User),
                TotalPrice = itemsWithDetails.Sum(i => i.Price * i.Quantity)
            };

            foreach (var item in itemsWithDetails)
            {
                order.Items.Add(new OrderItem
                {
                    Name = item.Name,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

            this.db.Add(order);
            this.db.SaveChanges();

            shoppingCartService.Clear(shoppingCartId);

            return View(new HomeIndexViewModel
            {
                Categories = await this.categories.All(),
                Producers = await this.producers.Brands()
            });
        }

        private List<CartItemDetailViewModel> GetCartItems(IEnumerable<CartItem> items)
        {
            var itemsId = items.Select(i => i.ProductId);

            var itemWithDetails = this.db.Products
                            .Where(p => itemsId.Contains(p.Id))
                            .Select(p => new CartItemDetailViewModel
                            {
                                ProductId = p.Id,
                                Name = p.Name,
                                Price = p.Price,
                                Weight = p.Weight
                            })
                            .ToList();

            var itemQuantities = items.ToDictionary(i => i.ProductId, i => i.Quantity);

            itemWithDetails.ForEach(i => i.Quantity = itemQuantities[i.ProductId]);

            return itemWithDetails;
        }
    }
}
