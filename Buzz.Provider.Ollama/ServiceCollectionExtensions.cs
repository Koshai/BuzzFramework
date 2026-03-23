using Buzz.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Buzz.Provider.Ollama;

/// <summary>
/// Dependency injection registration for the Ollama Buzz provider.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the Ollama provider with options bound from configuration.
    /// </summary>
    /// <param name="services">Target service collection.</param>
    /// <param name="configuration">Application configuration. Options are bound from the "Buzz:Ollama" section.</param>
    /// <param name="configure">Optional delegate to override or supplement bound values.</param>
    /// <returns>The same service collection for chaining.</returns>
    /// <remarks>Registers a named HttpClient "buzz-ollama" with BaseAddress from BaseUrl.</remarks>
    public static IServiceCollection AddBuzzOllama(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<OllamaBuzzOptions>? configure = null)
    {
        var builder = services.AddOptions<OllamaBuzzOptions>()
            .Bind(configuration.GetSection("Buzz:Ollama"));

        if (configure is not null)
        {
            builder.Configure(configure);
        }

        return AddBuzzOllamaCore(services);
    }

    /// <summary>
    /// Registers the Ollama provider with explicit options configuration.
    /// </summary>
    /// <param name="services">Target service collection.</param>
    /// <param name="configure">Options configuration (BaseUrl, Model).</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddBuzzOllama(
        this IServiceCollection services,
        Action<OllamaBuzzOptions> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        services.AddOptions<OllamaBuzzOptions>().Configure(configure);
        return AddBuzzOllamaCore(services);
    }

    private static IServiceCollection AddBuzzOllamaCore(IServiceCollection services)
    {
        services.AddHttpClient("buzz-ollama", static (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<OllamaBuzzOptions>>().Value;
            var baseUrl = string.IsNullOrWhiteSpace(options.BaseUrl)
                ? "http://localhost:11434/api/"
                : options.BaseUrl.TrimEnd('/') + "/";
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddScoped<IBuzzProvider>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var client = factory.CreateClient("buzz-ollama");
            var options = sp.GetRequiredService<IOptions<OllamaBuzzOptions>>().Value;
            return new OllamaBuzzProvider(client, options);
        });

        return services;
    }
}
