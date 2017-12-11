namespace GobelinsWorld.Web.Areas.Admin.Controllers
{
    using GobelinsWorld.Services.Admin;
    using GobelinsWorld.Web.Areas.Admin.Models.Products;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using GobelinsWorld.Web.Infrastructure.Extensions;
    using GobelinsWorld.Web.Controllers;

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

        public async Task<IActionResult> All()
        {
            var allProducts = await this.products.All();

            return View(allProducts);
        }

        public async Task<IActionResult> Create()
        {
            return View(new ProductFormModel
            {
                Producers = await this.GetProducersItems(),
                Categories = await this.GetCategoriesItems()
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

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await this.products.FindById(id);

            if (products == null)
            {
                return BadRequest();
            }

            return View(new ProductFormModel
            {
                Name = product.Name,
                Code = product.Code,
                Description = product.Description,
                Weight = product.Weight,
                Price = product.Price,
                ProducerId = product.ProducerId,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
                Producers = await this.GetProducersItems(),
                Categories = await this.GetCategoriesItems()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productExist = await this.products.IsExist(id);

            if (!productExist)
            {
                return NotFound();
            }

            await this.products.Edit(id, model.Name, model.Code, model.Weight, model.Price, model.Description, model.ImageUrl, model.ProducerId, model.CategoryId);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(id);
        }

        public async Task<IActionResult> Destroy(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productExist = await this.products.IsExist(id);

            if (!productExist)
            {
                return NotFound();
            }

            await this.products.Delete(id);

            return RedirectToAction(nameof(All));
        }

        private async Task<List<SelectListItem>> GetProducersItems()
        {
            var producers = await this.producers.All();

            var producersItems = producers
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

            var categoriesItems = categories
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();

            return categoriesItems;
        }

    }
}
