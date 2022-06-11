using Microsoft.Extensions.Logging;
using TakDotNet.Application.Common.Interfaces;

namespace TakDotNet.Infrastructure.Services;

public class EntryService : ICustomService
{
    private readonly ILogger<EntryService> _logger;

    public EntryService(ILogger<EntryService> logger)
    {
        _logger = logger;
    }

    public void Run(string[] args)
        => MainAsync(args).GetAwaiter().GetResult();

    private async Task MainAsync(string[] args)
    {
        await Log("Entry Service Running. Press ctrl+c to exit.");

        // block this task until the program is closed        
        await Task.Delay(Timeout.Infinite);
    }

    private Task Log(string message)
    {
        _logger.LogInformation(message);

        return Task.CompletedTask;
    }
}