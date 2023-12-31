﻿using var host = new HostBuilder().ConfigureHost();
using var scope = host.Services.CreateScope();

try
{
    var service = scope.ServiceProvider;
    service
        .GetRequiredService<MessageHandler>()
        .Start();
    await host.RunAsync();
}
catch (Exception exc)
{
    Console.WriteLine(exc.Message);
}

