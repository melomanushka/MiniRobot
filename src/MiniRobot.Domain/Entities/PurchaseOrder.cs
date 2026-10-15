namespace MiniRobot.Domain.Entities;

public class PurchaseOrder
{
    public Guid Id { get; set; }
    public string Number { get; set; } = null!;
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    //TODO: Status - после enums
    public List<PurchaseOrderLine> Lines { get; set; } = [];
}
