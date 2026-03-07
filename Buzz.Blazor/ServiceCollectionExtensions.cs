using Buzz.Core;
using Buzz.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Buzz.Blazor;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBuzzFramework(
        this IServiceCollection services,
        Action<BuzzOptions>? configure = null)
    {
        services.AddOptions<BuzzOptions>();
        if (configure is not null)
        {
            services.Configure(configure);
        }

        services.AddSingleton<IBuzzCaseMemoryStore, InMemoryBuzzCaseMemoryStore>();
        services.AddScoped<IBuzzHistoryStore, LocalStorageBuzzHistoryStore>();
        services.AddScoped<IBuzzSuggestionService, BuzzSuggestionService>();
        services.AddScoped<IBuzzClient, BuzzClient>();
        return services;
    }
}
