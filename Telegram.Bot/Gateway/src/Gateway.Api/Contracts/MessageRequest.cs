namespace MessageRelay.Contracts;

public sealed record MessageRequest
(
    MessageRequestId? Id,
    MessageRequestChatId? ChatId,
    MessageRequestUserId? UserId,
    string MessageFromFirstName,
    string MessageChatFirstName,
    string? Text,
    DateTime? Date
)
{
    // public static MessageRequest CreateMessageRequest()
    // {
    // }
}

public sealed record MessageRequestId(Guid Value);
public sealed record MessageRequestChatId(long? Value);
public sealed record MessageRequestUserId(long? Value);