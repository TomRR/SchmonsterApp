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
    public static MessageRequest CreateMessageRequest(Message? updateMessage)
    {
        if (updateMessage is null)
        {
            return null;
        }
        
        return new MessageRequest(
                new MessageRequestId(Guid.NewGuid()),
                new MessageRequestChatId(updateMessage.Chat?.Id),
                new MessageRequestUserId(updateMessage.From?.Id),
                updateMessage.From?.FirstName ?? string.Empty,
                updateMessage.Chat?.FirstName ?? string.Empty,
                updateMessage.Text,
                updateMessage.Date);
    }
}

public sealed record MessageRequestId(Guid Value);
public sealed record MessageRequestChatId(long? Value);
public sealed record MessageRequestUserId(long? Value);