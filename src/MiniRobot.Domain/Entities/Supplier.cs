namespace MiniRobot.Domain.Entities;

public class Supplier
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public List<PurchaseOrder> PurchaseOrders { get; set; } = [];

}
