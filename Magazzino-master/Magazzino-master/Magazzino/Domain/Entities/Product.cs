using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace Magazzino.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; } = 0;
    public string Sku { get; private set; }
    public int Quantities { get; set; }

    private Product(Guid id, string name, string description, decimal price, string sku, int quantities)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Sku = sku;
        Quantities = quantities;
    }
    protected Product() { }

    public static Product CreateProduct(string name, decimal price, string sku)
    {
        return new Product (Guid.NewGuid(),name,string.Empty, price, sku, 0);
    }

    public void UpdateQuantity (int  quantity)
    {
        if(quantity > 0)
        {
            this.Quantities = quantity;
        }else
        {
            throw new ArgumentOutOfRangeException(nameof(quantity));
        }
    }
}