namespace Magazzino.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();
}
