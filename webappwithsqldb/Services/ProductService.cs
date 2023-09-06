using System.Text.Json;
using webappwithsqldb.Models;

namespace webappwithsqldb.Services
{
    // This service will interact with our Product data in the SQL database
    public class ProductService : IProductService
    {

        public async Task<List<Product>> GetProducts()
        {
            String FunctionURL = "https://tempazurefunc2.azurewebsites.net/api/GetProduct?code=0h0RHrQ_WdU6gwzxN_fXYy7yWm1nqvU9E3Bc5biWmh4BAzFuZvsNCA==";

            using (HttpClient _client = new HttpClient())
            {
                HttpResponseMessage _response = await _client.GetAsync(FunctionURL);
                string _content = await _response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Product>>(_content);
            }
        }
    }
}
