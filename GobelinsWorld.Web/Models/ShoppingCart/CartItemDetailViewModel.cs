namespace GobelinsWorld.Web.Models.ShoppingCart
{
    public class CartItemDetailViewModel 
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }
    }
}
