namespace MiniRobot.Domain.Entities;

public class OperatorQuestion
{
    public Guid Id { get; set; }

    public Guid IncomingDocumentId { get; set; }
    public IncomingDocument IncomingDocument { get; set; } = null!;

    public string Text { get; set; } = null!;
    public string? Answer { get; set; }

    public DateTime AskedAt { get; set; }
    public DateTime? AnsweredAt { get; set; }
    // TODO: Status — после enums (открыт / отвечен)
}