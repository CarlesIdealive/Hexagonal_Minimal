using Domain.Entities;
using Domain.Ports.Primary;
using Domain.Ports.Secondary;

namespace Domain.Services
{
    public class ProductService : IService
    {
        //Orquestador de funcionamiento
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }




        public IEnumerable<Product> GetProducts()
        {
            return _repository.GetAllProducts();
        }

        public void Register(string name, decimal price)
        {
            _repository.Save(new Product(Guid.NewGuid(), name, price));
        }



    }
}
