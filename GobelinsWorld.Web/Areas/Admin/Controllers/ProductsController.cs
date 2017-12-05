using GobelinsWorld.Services.Admin;
using GobelinsWorld.Web.Areas.Admin.Models.Products;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using GobelinsWorld.Web.Infrastructure.Extensions;
using GobelinsWorld.Web.Controllers;

namespace GobelinsWorld.Web.Areas.Admin.Controllers
{

    public class ProductsController : BaseAdminController
    {
        private readonly IProductService products;
        private readonly IProducerService producers;
        private readonly ICategoryService categories;

        public ProductsController(IProductService products, IProducerService producers, ICategoryService categories)
        {
            this.products = products;
            this.producers = producers;
            this.categories = categories;
        }

        public async Task<IActionResult> Create()
        {
            return View(new ProductFormModel
            {
                Producers = await this.GetProducersItems(),
                Categories=await this.GetCategoriesItems()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Producers = await this.GetProducersItems();
                model.Categories = await this.GetCategoriesItems();

                return View(model);
            }

            await this.products.Create(model.Name, model.Code, model.Weight, model.Price, model.Description, model.ImageUrl, model.ProducerId, model.CategoryId);

            TempData.AddSuccessMessage($"Product {model.Name} created successfully.");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        private async Task<List<SelectListItem>> GetProducersItems()
        {
            var producers = await this.producers.All();

            var producersItems=producers
                .Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                }).ToList();

            return producersItems;
        }

        private async Task<List<SelectListItem>> GetCategoriesItems()
        {
            var categories = await this.categories.All();

            var categoriesItems= categories
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();

            return categoriesItems;
        }

    }
}
