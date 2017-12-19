namespace GobelinsWorld.Web.Controllers
{
    using GobelinsWorld.Services.Admin;
    using GobelinsWorld.Services.User;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.ProductViewModels;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IUserProductService products;
        private readonly ICategoryService categories;
        private readonly IProducerService producers;

        public HomeController(IUserProductService products, ICategoryService categories, IProducerService producers)
        {
            this.products = products;
            this.categories = categories;
            this.producers = producers;
        }

        public async Task<IActionResult> Index()
        {        
            return base.View(new ProductIndexListingViewModel {
                Products = await this.products.New(),
                Categories = await this.categories.All(),
                Producers = await this.producers.Brands(),
            });
        }

        public async Task<IActionResult> About()
        {
            return View(await GetCategoriesAndProducers());
        }

        public async Task<IActionResult> Contact()
        {
            return View(await GetCategoriesAndProducers());
        }

        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel {
                Categories = await this.categories.All(),
                Producers = await this.producers.Brands(),
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        private async Task<HomeIndexViewModel> GetCategoriesAndProducers()
        {
            return new HomeIndexViewModel
            {
                Categories = await this.categories.All(),
                Producers = await this.producers.Brands()
            };
        }
    }
}
