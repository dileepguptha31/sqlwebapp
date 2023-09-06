using webappwithsqldb.Models;

namespace webappwithsqldb.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetProducts();
    }
}
