using Domain.Entities;
using Domain.Ports.Primary;
using Domain.Ports.Secondary;
using Domain.Services;
using JSonRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Dependencia para el documento que utilizamos como repositorio de productos, en este caso un archivo JSON
string pathFile = Path.Combine(AppContext.BaseDirectory, "products.json");
// Agregamos el servicio de repositorio de productos utilizando la implementación que lee desde un archivo JSON
builder.Services.AddTransient<IRepository>(provider => new ProductRepository(pathFile));    // Puerto Secundario con Implementación de repositorio
builder.Services.AddTransient<IService, ProductService>();  // Puerto Primario con Adaptador de servicio

//Configuramos Swagger para que se muestre en la raíz de la aplicación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Esto hará que Swagger UI esté disponible en la raíz
    });

}

app.UseHttpsRedirection();


//Creamos un endpoint para obtener todos los productos utilizando el servicio de productos
app.MapGet("/products", (IService productService) =>
{
    var products = productService.GetProducts();
    return Results.Ok(products);
});
app.MapPost("/products", (IService productService, string name, decimal price) =>
{
    productService.Register(name, price);
    return Results.Created();
}).WithName("CreateProduct");


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
