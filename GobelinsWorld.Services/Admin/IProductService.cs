namespace GobelinsWorld.Services.Admin
{
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task Create(string name, string code, double weight, decimal price, string description,
             string imageUrl,int producerId, int categoryId );        
    }
}
