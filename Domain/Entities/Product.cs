namespace Domain.Entities;

public class Product
{
    private string _name;
    private decimal _price;

    public Guid Id { get; set; }
    public string Name { 
        get => _name; 
        set {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }
            _name = value;
        }
    }
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Product price cannot be negative.");
            }
            _price = value;
        }
    }


    public Product()
    {
        
    }


    public Product(Guid id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }




}
