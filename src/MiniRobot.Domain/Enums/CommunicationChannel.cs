namespace MiniRobot.Domain.Enums;

public enum CommunicationChannel
{
    /// <summary>Пришёл через мессенджер</summary>
    Messenger = 1,

    /// <summary>Пришёл по почте</summary>
    Mail = 2,

    /// <summary>Загружен человеком вручную (например, после звонка)</summary>
    Manual = 3,
}