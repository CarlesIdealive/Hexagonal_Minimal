using Domain.Entities;

namespace Domain.Ports.Secondary;

public interface IRepository
{
    List<Product> GetAllProducts();
    void Save(Product product);


}
