using Buzz.Blazor.Providers;
using Buzz.Core;
using Buzz.Blazor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Buzz.Blazor;

/// <summary>
/// Dependency injection registration helpers for Buzz Blazor services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers core Buzz services with options bound from configuration.
    /// </summary>
    /// <param name="services">Target service collection.</param>
    /// <param name="configuration">Application configuration. Buzz options are bound from the "Buzz" section.</param>
    /// <param name="configure">Optional delegate to override or supplement bound values.</param>
    /// <returns>The same service collection for chaining.</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddBuzzFramework(builder.Configuration);
    /// // Or with overrides:
    /// builder.Services.AddBuzzFramework(builder.Configuration, o => o.DefaultProviderName = "ollama");
    /// </code>
    /// </example>
    public static IServiceCollection AddBuzzFramework(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<BuzzOptions>? configure = null)
    {
        var builder = services.AddOptions<BuzzOptions>()
            .Bind(configuration.GetSection("Buzz"));

        if (configure is not null)
        {
            builder.Configure(configure);
        }

        return AddBuzzFrameworkCore(services);
    }

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

        return AddBuzzFrameworkCore(services);
    }

    private static IServiceCollection AddBuzzFrameworkCore(IServiceCollection services)
    {
        services.AddSingleton<IBuzzCaseMemoryStore, InMemoryBuzzCaseMemoryStore>();
        services.TryAddSingleton<IBuzzSeedKnowledgeStore, JsonBuzzSeedKnowledgeStore>();
        services.AddScoped<IBuzzHistoryStore, LocalStorageBuzzHistoryStore>();
        services.AddScoped<IBuzzSuggestionService, BuzzSuggestionService>();
        services.AddScoped<IBuzzOptionRanker, BuzzOptionRanker>();
        services.AddScoped<IBuzzToggleAdvisor, BuzzToggleAdvisor>();
        services.AddScoped<IBuzzAiContextComposer, BuzzAiContextComposer>();
        services.AddScoped<IBuzzClient, BuzzClient>();
        return services;
    }

    /// <summary>
    /// Registers the mock Buzz provider for testing and fallback when no real AI is configured.
    /// </summary>
    /// <param name="services">Target service collection.</param>
    /// <returns>The same service collection for chaining.</returns>
    /// <remarks>Echoes input as output. Use when OpenAI/Ollama are unavailable or for development.</remarks>
    public static IServiceCollection AddBuzzMock(this IServiceCollection services)
    {
        services.AddScoped<IBuzzProvider, MockBuzzProvider>();
        return services;
    }
}
