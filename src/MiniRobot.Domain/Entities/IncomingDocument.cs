namespace MiniRobot.Domain.Entities;

    public class IncomingDocument
    {
        public Guid Id { get; set; }

        // TODO: Channel — после enums (почта, мессенджер...)
        public string ExternalMessageId { get; set; } = null!;   // чтобы не обработать одно сообщение дважды

        public string? StorageKey { get; set; }        // ключ файла в MinIO; null — если вложения нет
        public string? OriginalFileName { get; set; }

        public Guid? SupplierId { get; set; }          // null — отправитель ещё не опознан
        public Supplier? Supplier { get; set; }

        public Guid? PurchaseOrderId { get; set; }     // null — заказ ещё не найден
        public PurchaseOrder? PurchaseOrder { get; set; }

        public DateTime ReceivedAt { get; set; }
        // TODO: Status — после enums

        public List<IncomingDocumentLine> Lines { get; set; } = [];
        public List<OperatorQuestion> Questions { get; set; } = [];
    }
