namespace GobelinsWorld.Web.Areas.Admin.Controllers
{
    using GobelinsWorld.Services.Admin;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class OrdersController : BaseAdminController
    {
        private readonly IOrderService orders;

        public OrdersController(IOrderService orders)
        {
            this.orders = orders;
        }

        public async Task<IActionResult> All()
        {
            var allOrders = await this.orders.All();

            return View(allOrders);
        }
    }
}
