using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobelinsWorld.Web.Models.ShoppingCart
{
    public class CartItemListingViewModel : HomeIndexViewModel
    {
       public IEnumerable<CartItemDetailViewModel> Products { get; set; }
    }
}
