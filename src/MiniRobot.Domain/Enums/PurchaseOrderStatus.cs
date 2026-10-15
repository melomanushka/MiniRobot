namespace MiniRobot.Domain.Enums;

/// <summary>
/// Статус заказа поставщику.
/// Путь: Created → SentToSupplier → PartiallyReceived → FullyReceived → Posted.
/// Отменить можно до полного получения.
/// </summary>
public enum PurchaseOrderStatus
{
    /// <summary>Заказ создан, но ещё не отправлен поставщику</summary>
    Created = 1,

    /// <summary>Заказ отправлен поставщику, ждём документы и товар</summary>
    SentToSupplier = 2,

    /// <summary>Получена часть товара по заказу</summary>
    PartiallyReceived = 3,

    /// <summary>Получен весь товар по заказу</summary>
    FullyReceived = 4,

    /// <summary>Заказ проведён. Конечный статус</summary>
    Posted = 5,

    /// <summary>Заказ отменён. Конечный статус</summary>
    Cancelled = 6,
}