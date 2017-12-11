using GobelinsWorld.Services.Admin;
using GobelinsWorld.Web.Models.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobelinsWorld.Web.Controllers
{
    public class ProductsController : Controller
    {
        private const int PageSize = 25;

        private readonly IProductService products;

        public ProductsController(IProductService products)
        {
            this.products = products;
        }

        public async Task<IActionResult> All(int id,int page=1)
        {
            var allProducts = await this.products.AllByCategory(id, page, PageSize);
            
            return View(new ProductPageListingViewModel {
                Products=allProducts,
                CurrentPage=page,
                TotalPages=(int)Math.Ceiling(this.products.Total()/(double)PageSize)
            });
        }

    }
}
