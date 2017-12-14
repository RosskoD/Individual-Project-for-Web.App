namespace GobelinsWorld.Web.Controllers
{
    using GobelinsWorld.Services.Admin;
    using GobelinsWorld.Services.User;
    using Microsoft.AspNetCore.Mvc;
    using Models.ProductViewModels;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GobelinsWorld.Services.User.Models;

    public class ProductsController : Controller
    {
        private const int PageSize = 12;

        private readonly IUserProductService products;
        private readonly ICategoryService categories;
        private readonly IProducerService producers;

        public ProductsController(IUserProductService products, ICategoryService categories, IProducerService producers)
        {
            this.products = products;
            this.categories = categories;
            this.producers = producers;
        }        

        public async Task<IActionResult> ByCategory(int id, int page = 1)
        {
            var allProducts = await this.products.AllByCategory(id, page, PageSize);

            return base.View("All", await GetProducts(page, allProducts));
        }

        public async Task<IActionResult> ByProducer(int id, int page = 1)
        {
            var allProducts = await this.products.AllByProducer(id, page, PageSize);

            return View("All", await GetProducts(page, allProducts));
        }


        public async Task<IActionResult> Detail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = await this.products.Detail(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(new ProductDetailViewModel
            {
                Product = product,
                Categories = await this.categories.All(),
                Producers = await this.producers.Brands(),
            });
        }

        private async Task<ProductPageListingViewModel> GetProducts(int page, IEnumerable<UserProductListingServiceModel> allProducts)
        {
            return new ProductPageListingViewModel
            {
                Categories = await this.categories.All(),
                Producers = await this.producers.Brands(),
                Products = allProducts,
                CurrentPage = page,
                TotalProducts=this.products.Total(),
                TotalPages = (int)Math.Ceiling(this.products.Total() / (double)PageSize)
            };
        }
    }
}
