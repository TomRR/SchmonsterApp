namespace MessageRelay.Configurations;

internal static class ClientsConfiguration
{
    internal static IServiceCollection RegisterClients(this IServiceCollection services)
    {
        services.AddHttpClient<IGatewayClient, GatewayClient>((serviceProvider, client) =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<GatewaySetting>>().Value;

            client.BaseAddress = new Uri(settings.Uri);
        });

        return services;
    }
}