namespace GobelinsWorld.Services.ShoppingCart
{
    using Models;
    using System.Collections.Generic;

    public interface IShoppingCartService
    {
        void AddToCart(string id, int productId);

        void EditQuantity(string id, int productId, int quantity);

        void RemoveFromCart(string id,int productId);

        void Clear(string id);

        IEnumerable<CartItem> GetItems(string id);
        
    }
}
