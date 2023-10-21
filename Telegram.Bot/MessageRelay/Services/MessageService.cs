namespace MessageRelay.Services;

public interface IMessageService
{
    Task<MessageResponse?> HandleMessageUpdateAsync(MessageRequest request);
}
public sealed class MessageService : IMessageService
{
    private readonly IGatewayClient _client;

    public MessageService(IGatewayClient client)
    {
        _client = client ?? throw new ArgumentException(nameof(client), nameof(IGatewayClient));
    }
    
    public async Task<MessageResponse?> HandleMessageUpdateAsync(MessageRequest? request)
    {

        if (request?.ChatId is null)
        {
            return null;
        }
        var result = await _client.SendMessage(request);

        return result ?? MessageResponse.CreateRequestErrorMessage(request.ChatId);
    }
}