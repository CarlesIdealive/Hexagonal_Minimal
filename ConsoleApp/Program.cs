using Domain.Ports.Primary;
using Domain.Ports.Secondary;
using Domain.Services;  // Adapter primary
using JSonRepository;   // Adapter secondary
using Microsoft.Extensions.DependencyInjection;

string pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.json");

var services = new ServiceCollection();
services.AddTransient<IRepository>(provider => new ProductRepository(pathFile));
services.AddTransient<IService, ProductService>();

var serviceProvider = services.BuildServiceProvider();
var productService = serviceProvider.GetService<IService>();    // Resolve the service a través de la interfaz

while (true)
{
    try { 
        Console.WriteLine("Seleccione una opcion:");
        Console.WriteLine("1. Agregar producto");
        Console.WriteLine("2. Listar productos");
        Console.WriteLine("3. Limpiar consola");
        Console.WriteLine("4. Salir");
        Console.Write("Opcion: ");
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                Console.Write("Ingrese el nombre del producto:");
                string name = Console.ReadLine();
                Console.Write("Ingrese el precio del producto:");
                decimal price = decimal.Parse(Console.ReadLine());
                productService.Register(name, price);
                break;
            case "2":
                Console.WriteLine("Productos registrados:");
                foreach (var product in productService.GetProducts())
                {
                    Console.WriteLine($"- {product.Name}: {product.Price}");
                }
                break;
            case "3":
                Console.Clear();
                break;
            case "4":
                Console.WriteLine("Saliendo...");
                return;
            default:
                Console.WriteLine("Opcion no valida. Intente de nuevo.");
                break;
        }


    
    } catch (Exception ex) { 
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}