namespace MessageRelay.Contracts;

public sealed record MessageResponse(MessageRequestChatId ChatId, string Text)
{
    public static MessageResponse CreateRequestErrorMessage(MessageRequestChatId chatId)
    {
        return new MessageResponse(chatId, $"Sorry, there are some technical difficulties at the moment. Please try again later...");
    }    
}