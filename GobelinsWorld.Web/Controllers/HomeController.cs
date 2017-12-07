namespace GobelinsWorld.Web.Controllers
{
    using GobelinsWorld.Services.Admin;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ICategoryService categories;
        private readonly IProducerService producers;

        public HomeController(ICategoryService categories, IProducerService producers)
        {
            this.categories = categories;
            this.producers = producers;
        }

        public async Task<IActionResult> Index()
        {
            var allCategories = await this.categories.All();
            var allProducers = await this.producers.Brands();

            return View(new HomeIndexViewModel
            {
                Categories = allCategories,
                Producers=allProducers
            });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
