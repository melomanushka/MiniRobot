namespace MiniRobot.Domain.Entities;

public class IncomingDocumentLine
{
    public Guid Id { get; set; }
    public Guid IncomingDocumentId { get; set; }
    public IncomingDocument IncomingDocument { get; set; } = null!;

    public string Sku { get; set; } = null!;
    public string? Name { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
