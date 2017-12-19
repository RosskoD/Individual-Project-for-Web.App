namespace GobelinsWorld.Services.ShoppingCart.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCart
    {
        private readonly IList<CartItem> items;

        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }

        public void AddItem(int productId, int quantity)
        {
            var item = this.items.FirstOrDefault(i => i.ProductId == productId);

            if (item==null)
            {
                item = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity==0 ? 1: quantity
                };

                this.items.Add(item);
            }
            else
            {
                item.Quantity++;
            }
        }

        public void EdiItemQuantity(int productId, int quantity)
        {
            var item = this.items.FirstOrDefault(p => p.ProductId == productId);

            if (item == null)
            {
                return;
            }

            item.Quantity = quantity;
        }


        public void RemoveItem(int productId)
        {
            var item = this.items
                .FirstOrDefault(p=>p.ProductId==productId);

            if (item==null)
            {
                return;
            }

            this.items.Remove(item);
        }

        public void Clear()
        {
            this.items.Clear();
        }
        public IEnumerable<CartItem> Items=> new List<CartItem>(this.items);
        
    }
}
