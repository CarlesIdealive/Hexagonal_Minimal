using Domain.Entities;
using Domain.Ports.Secondary;
using System.Text.Json;

namespace JSonRepository
{
    public class ProductRepository : IRepository
    {
        private readonly string _filePath;
        public ProductRepository(string path)
        {
            _filePath = path;
        }



        public List<Product> GetAllProducts()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Product>();
            }
            var products = JsonSerializer.Deserialize<List<Product>>(File.ReadAllText(_filePath));
            return products ?? [];
        }

        public void Save(Product product)
        {
            var products = GetAllProducts();
            products.Add(product);
            var options = new JsonSerializerOptions { WriteIndented = true };   // Optional: for better readability
            var json = JsonSerializer.Serialize(products, options);
            File.WriteAllText(_filePath, json);
        }

         
    }
}
