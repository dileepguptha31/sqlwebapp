using webappwithsqldb.Models;

namespace webappwithsqldb.Services
{
    public interface IProductService
    {
        public List<Product> GetProducts();
    }
}
