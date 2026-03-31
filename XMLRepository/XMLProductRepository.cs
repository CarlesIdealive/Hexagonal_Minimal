using Domain.Entities;
using Domain.Ports.Secondary;
using System.Xml.Linq;

namespace XMLRepository
{
    public class XMLProductRepository : IRepository
    {

        private readonly string _filePath;

        public XMLProductRepository(string filePath )
        {
            _filePath = filePath;
            if (!File.Exists(_filePath) || new FileInfo(_filePath).Length == 0)
            {
                // Si el archivo no existe, lo creamos con una estructura básica
                var initialData = new List<Product>();
                var root = new XElement("Products");
                var doc = new XDocument(root);
                doc.Save(_filePath);
            }
        }




        public List<Product> GetAllProducts()
        {
            var doc = XDocument.Load(_filePath);
            var products = new List<Product>();
            foreach (var element in doc.Descendants("Product"))
            {
                var product = new Product
                {
                    Id = Guid.Parse(element.Element("Id")?.Value ?? Guid.NewGuid().ToString()),
                    Name = element.Element("Name").Value,
                    Price = decimal.Parse(element.Element("Price")?.Value ?? "0")
                };
                products.Add(product);
            }
            return products;
        }




        public void Save(Product product)
        {
            var doc = XDocument.Load(_filePath);
            var productElement = new XElement("Product",
                new XElement("Id", product.Id),
                new XElement("Name", product.Name),
                new XElement("Price", product.Price)
            );
            doc.Root.Add(productElement);
            doc.Save(_filePath);
        }
    }
}
