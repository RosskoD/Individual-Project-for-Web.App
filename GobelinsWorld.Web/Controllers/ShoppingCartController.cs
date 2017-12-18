namespace GobelinsWorld.Web.Controllers
{
    using Data;
    using GobelinsWorld.Services.Admin;
    using GobelinsWorld.Services.ShoppingCart;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.ShoppingCart;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly GobelinsWorldDbContext db;
        private readonly ICategoryService categories;
        private readonly IProducerService producers;

        public ShoppingCartController(IShoppingCartService shoppingCartService, GobelinsWorldDbContext db, ICategoryService categories, IProducerService producers)
        {
            this.shoppingCartService = shoppingCartService;
            this.db = db;
            this.categories = categories;
            this.producers = producers;
        }

        public IActionResult AddToCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartService.AddToCart(shoppingCartId, id);

            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveFromCart(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartService.RemoveFromCart(shoppingCartId, id);

            return RedirectToAction(nameof(Details));
        }

        public IActionResult Clear(int id)
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            this.shoppingCartService.Clear(shoppingCartId);

            return RedirectToAction(nameof(Details));
        }

        public async Task<IActionResult> Details()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var items = this.shoppingCartService.GetItems(shoppingCartId);

            var itemsId = items.Select(i => i.ProductId);

            var itemQuantities = items.ToDictionary(i => i.ProductId, i => i.Quantity);

            var allCategories = await this.categories.All();
            var allProducers = await this.producers.Brands();

            var itemsWithDetails = this.db.Products
                .Where(p => itemsId.Contains(p.Id))
                .Select(p => new CartItemDetailViewModel
                {
                    ProductId = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Weight = p.Weight
                })
                .ToList();

            itemsWithDetails.ForEach(i => i.Quantity = itemQuantities[i.ProductId]);

            return View(new CartItemListingViewModel
            {
                Products = itemsWithDetails,
                Categories = allCategories,
                Producers = allProducers
            });
        }

        [Authorize]
        public IActionResult FinishOrder()
        {
            return RedirectToAction("/");
        }
    }
}
