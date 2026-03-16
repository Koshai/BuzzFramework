using Buzz.Core;
using Buzz.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Buzz.Blazor;

/// <summary>
/// Dependency injection registration helpers for Buzz Blazor services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers core Buzz services required by AI-assisted components.
    /// </summary>
    /// <param name="services">Target service collection.</param>
    /// <param name="configure">Optional options delegate for <see cref="BuzzOptions"/>.</param>
    /// <returns>The same service collection for chaining.</returns>
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
        services.AddScoped<IBuzzOptionRanker, BuzzOptionRanker>();
        services.AddScoped<IBuzzToggleAdvisor, BuzzToggleAdvisor>();
        services.AddScoped<IBuzzClient, BuzzClient>();
        return services;
    }
}
