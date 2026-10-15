using MiniRobot.Domain.Enums;

namespace MiniRobot.Domain.Entities;

public class IncomingDocument
{
    public Guid Id { get; set; }

    public CommunicationChannel Channel { get; set; }
    public string ExternalMessageId { get; set; } = null!;   // чтобы не обработать одно сообщение дважды

    public string? StorageKey { get; set; }        // ключ файла в MinIO; null — если вложения нет
    public string? OriginalFileName { get; set; }

    public Guid? SupplierId { get; set; }          // null — отправитель ещё не опознан
    public Supplier? Supplier { get; set; }

    public Guid? PurchaseOrderId { get; set; }     // null — заказ ещё не найден
    public PurchaseOrder? PurchaseOrder { get; set; }

    public DateTime ReceivedAt { get; set; }
    public IncomingDocumentStatus Status { get; set; }

    public List<IncomingDocumentLine> Lines { get; set; } = [];
    public List<OperatorQuestion> Questions { get; set; } = [];
    
    /// <summary>
    /// Ошибка последней попытки
    /// </summary>
    public string? LastError { get; set; }
}
