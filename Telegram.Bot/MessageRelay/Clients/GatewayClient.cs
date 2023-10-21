namespace MessageRelay.Clients;

public interface IGatewayClient
{
    Task<MessageResponse?> SendMessage(MessageRequest request);
}
internal sealed class GatewayClient : IGatewayClient
{
    private readonly HttpClient _httpClient;
    private readonly string _requestUri;
    public GatewayClient(HttpClient httpClient, IOptions<GatewaySetting> options)
    {
        _httpClient = httpClient;
        _requestUri = options.Value.RequestUri;
    }

    public async Task<MessageResponse?> SendMessage(MessageRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(_requestUri, request);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<MessageResponse>();
    }
}