namespace GobelinsWorld.Services.ShoppingCart
{
    using Models;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> carts;

        public ShoppingCartService()
        {
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string id, int productId, int quantity)
        {
            ShoppingCart shoppingCart = this.GetShoppingCart(id);

            shoppingCart.AddItem(productId, quantity);
        }

        public void EditQuantity(string id, int productId, int quantity)
        {
            ShoppingCart shoppingCart = this.GetShoppingCart(id);

            shoppingCart.EdiItemQuantity(productId, quantity);
        }

        public void RemoveFromCart(string id, int productId)
        {
            ShoppingCart shoppingCart = this.GetShoppingCart(id);

            shoppingCart.RemoveItem(productId);            
        }

        public void Clear(string id)
        {
            ShoppingCart shoppingCart = this.GetShoppingCart(id);

            shoppingCart.Clear();
           
        }

        public IEnumerable<CartItem> GetItems(string id)
        {
            ShoppingCart shoppingCart = this.GetShoppingCart(id);

            return new List<CartItem>(shoppingCart.Items);            
        }
               
        private ShoppingCart GetShoppingCart(string id)
        {
            return this.carts.GetOrAdd(id, new ShoppingCart());
        }
    }
}
