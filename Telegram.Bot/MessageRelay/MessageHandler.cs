namespace MessageRelay;

internal sealed class MessageHandler
{
    private readonly IMessageService _service;
    private readonly TelegramBotClient _client;
    private readonly long _chatId;
    private readonly ReceiverOptions _receiverOptions = new ReceiverOptions()
    {
        Offset = null,
        AllowedUpdates = new UpdateType[] { },
        Limit = null,
        ThrowPendingUpdates = false
    };

    public MessageHandler(IMessageService messageService, IOptions<TelegramBotClientSetting> settings)
    {
        _service = messageService ?? throw new ArgumentNullException(nameof(messageService));
        var token = settings?.Value?.Token ?? throw new ArgumentNullException(nameof(TelegramBotClientSetting));
        _chatId = settings?.Value?.ChatId ?? throw new ArgumentNullException(nameof(TelegramBotClientSetting));
        _client = new TelegramBotClient(token);
    }

    public void Start()
    {
        _client.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions);
    }

    private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (!ChatIdsAreEquals(update.Message?.Chat?.Id ?? 0))
        {
            await client.SendTextMessageAsync(_chatId, MessageConstants.ChatIdsNotAllowed, cancellationToken: token);
            return;
        }
        
        await client.SendTextMessageAsync(_chatId, MessageConstants.GotIt, cancellationToken: token);
        
        if (token.IsCancellationRequested && !ReceivedTextMessage(update) && update?.Message is null)
        {
            return;
        }

        var messageRequest = MessageRequest.CreateMessageRequest(update?.Message);

        try
        {
            var response = await _service.HandleMessageUpdateAsync(messageRequest);
            var text = response?.Text ?? MessageConstants.NoResponseText;
            await client.SendTextMessageAsync(_chatId, text, cancellationToken: token);
        }
        catch (Exception ex)
        {
            await client.SendTextMessageAsync(_chatId, MessageConstants.ExceptionSendingMessage, cancellationToken: token);
            return;
        }


    }
    
    private async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        await client.SendTextMessageAsync(
            _chatId, 
            MessageConstants.BotClientErrorHandleException(exception, nameof(MessageHandler)), 
                cancellationToken: token);
    }

    private bool ChatIdsAreEquals(long chatId)
    {
        return _chatId.Equals(chatId);
    }

    private static bool ReceivedTextMessage(Update update) =>
        update.Type is UpdateType.Message && 
        update?.Message?.Type is MessageType.Text;
}