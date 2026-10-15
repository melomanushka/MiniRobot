namespace MiniRobot.Domain.Entities;

public class PurchaseOrderLine
{
    public Guid Id { get; set; }
    public Guid PurchaseOrderId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; } = null!;

    public string Sku { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }

}
