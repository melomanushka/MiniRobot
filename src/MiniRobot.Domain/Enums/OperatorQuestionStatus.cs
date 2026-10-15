namespace MiniRobot.Domain.Enums;

/// <summary>
/// Статус вопроса, который робот задал человеку.
/// Путь: Open → (Answered | Cancelled).
/// </summary>
public enum OperatorQuestionStatus
{
    /// <summary>Вопрос задан, ждёт ответа человека</summary>
    Open = 1,

    /// <summary>Человек ответил. Ответ — в OperatorQuestion.Answer</summary>
    Answered = 2,

    /// <summary>Вопрос стал неактуален без ответа (например, поставщик прислал исправленный документ)</summary>
    Cancelled = 3,
}