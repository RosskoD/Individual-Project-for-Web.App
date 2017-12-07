namespace GobelinsWorld.Web.Areas.Admin.Controllers
{
    using GobelinsWorld.Services.Admin;
    using GobelinsWorld.Services.Admin.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CategoriesController : BaseAdminController
    {
        private readonly ICategoryService categories;

        public CategoriesController(ICategoryService categories)
        {
            this.categories = categories;
        }
        
        public async Task<IActionResult> All()
        {
            var allCategories = await this.categories.All();

            return View(allCategories);
        }

        public IActionResult Create()
        {
             return  View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.categories.Create(model.Name);

            TempData.AddSuccessMessage($"Category {model.Name} created successfully.");

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await this.categories.FindById(id);

            if (category==null)
            {
                return BadRequest();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryFormServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categoryExist =await this.categories.Exist(id);

            if (!categoryExist)
            {
                return NotFound();
            }

            await this.categories.Edit(id, model.Name);

            return RedirectToAction(nameof(All));
        }
                
        public IActionResult Delete(int id)
        {
            return  View(id);
        }
        
        public async Task<IActionResult> Destroy(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var categoryExist = await this.categories.Exist(id);

            if (!categoryExist)
            {
                return NotFound();
            }

            await this.categories.Delete(id);

            return RedirectToAction(nameof(All));
        }       
    }
}
