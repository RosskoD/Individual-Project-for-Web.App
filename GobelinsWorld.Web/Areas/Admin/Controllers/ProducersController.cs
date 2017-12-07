namespace GobelinsWorld.Web.Areas.Admin.Controllers
{
    using GobelinsWorld.Services.Admin;
    using GobelinsWorld.Services.Admin.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ProducersController : BaseAdminController
    {
        private readonly IProducerService producers;

        public ProducersController(IProducerService producers)
        {
            this.producers = producers;
        }

        public async Task<IActionResult> All()
        {
            var allProducers = await this.producers.All();

            return View(allProducers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProducerFormServiceModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await this.producers.Create(model.Name, model.LogoUrl);

            TempData.AddSuccessMessage($"Producer {model.Name} created successfully.");

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producer = await this.producers.FindById(id);

            if (producer == null)
            {
                return BadRequest();
            }

            return View(producer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProducerFormServiceModel model)
        {
            var producerExist = await this.producers.IsExist(id);

            if (!producerExist)
            {
                return NotFound();
            }

            await this.producers.Edit(id, model.Name, model.LogoUrl);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            return View(id);
        }

        public async Task<IActionResult> Destroy(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var producer = await this.producers.FindById(id);

            if (producer == null)
            {
                return BadRequest();
            }

            await this.producers.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
