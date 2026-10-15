namespace MiniRobot.Domain.Enums;

/// <summary>
/// Статус входящего документа от поставщика.
/// Путь: Received → (Posted | WaitingForOperator | Failed),
/// WaitingForOperator → (ApprovedByOperator → Posted | RejectedByOperator).
/// </summary>
public enum IncomingDocumentStatus
{
    /// <summary>Документ получен и ждёт обработки роботом</summary>
    Received = 1,

    /// <summary>Ошибка обработки (нет вложения, неподдерживаемый формат, технический сбой). Причина — в IncomingDocument.LastError</summary>
    Failed = 2,

    /// <summary>Ждёт решения человека: найдено расхождение с заказом или заказ не найден</summary>
    WaitingForOperator = 3,

    /// <summary>Человек отклонил документ. Конечный статус</summary>
    RejectedByOperator = 4,

    /// <summary>Человек одобрил документ, робот может его провести</summary>
    ApprovedByOperator = 5,

    /// <summary>Документ проведён. Конечный статус</summary>
    Posted = 6,
}