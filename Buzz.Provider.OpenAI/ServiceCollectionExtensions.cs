using Buzz.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Buzz.Provider.OpenAI;

/// <summary>
/// Dependency injection registration for the OpenAI Buzz provider.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the OpenAI provider with options bound from configuration.
    /// </summary>
    /// <param name="services">Target service collection.</param>
    /// <param name="configuration">Application configuration. Options are bound from the "Buzz:OpenAI" section.</param>
    /// <param name="configure">Optional delegate to override or supplement bound values (e.g. set ApiKey from environment).</param>
    /// <returns>The same service collection for chaining.</returns>
    /// <remarks>Registers a named HttpClient "buzz-openai". ApiKey can be set via config or <c>OPENAI_API_KEY</c> environment variable.</remarks>
    public static IServiceCollection AddBuzzOpenAI(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<OpenAiBuzzOptions>? configure = null)
    {
        var builder = services.AddOptions<OpenAiBuzzOptions>()
            .Bind(configuration.GetSection("Buzz:OpenAI"));

        if (configure is not null)
        {
            builder.Configure(configure);
        }

        return AddBuzzOpenAICore(services);
    }

    /// <summary>
    /// Registers the OpenAI provider with explicit options configuration.
    /// </summary>
    /// <param name="services">Target service collection.</param>
    /// <param name="configure">Options configuration. ApiKey is required.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddBuzzOpenAI(
        this IServiceCollection services,
        Action<OpenAiBuzzOptions> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        services.AddOptions<OpenAiBuzzOptions>().Configure(configure);
        return AddBuzzOpenAICore(services);
    }

    private static IServiceCollection AddBuzzOpenAICore(IServiceCollection services)
    {
        services.AddHttpClient("buzz-openai", static (sp, client) =>
        {
            client.BaseAddress = new Uri("https://api.openai.com/v1/");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddScoped<IBuzzProvider>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var client = factory.CreateClient("buzz-openai");
            var options = sp.GetRequiredService<IOptions<OpenAiBuzzOptions>>().Value;
            var apiKey = options.ApiKey ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? string.Empty;
            var resolvedOptions = new OpenAiBuzzOptions
            {
                ApiKey = apiKey,
                Model = options.Model,
                MaxOutputTokens = options.MaxOutputTokens,
                Temperature = options.Temperature
            };
            return new OpenAiBuzzProvider(client, resolvedOptions);
        });

        return services;
    }
}
