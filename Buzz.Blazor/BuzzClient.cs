using Buzz.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Buzz.Blazor;

internal sealed class BuzzClient : IBuzzClient
{
    private readonly IReadOnlyDictionary<string, IBuzzProvider> _providers;
    private readonly BuzzOptions _options;
    private readonly ILogger<BuzzClient> _logger;

    public BuzzClient(
        IEnumerable<IBuzzProvider> providers,
        IOptions<BuzzOptions> options,
        ILogger<BuzzClient> logger)
    {
        _providers = providers.ToDictionary(
            provider => provider.Name,
            StringComparer.OrdinalIgnoreCase);
        _options = options.Value;
        _logger = logger;
    }

    public async Task<BuzzResponse> GenerateAsync(BuzzRequest request, CancellationToken cancellationToken = default)
    {
        if (_providers.Count == 0)
        {
            throw new InvalidOperationException(
                "No IBuzzProvider has been registered. Register at least one provider in DI.");
        }

        var providerNames = BuildProviderPreference();
        var failures = new List<string>();

        foreach (var providerName in providerNames)
        {
            if (!_providers.TryGetValue(providerName, out var provider))
            {
                continue;
            }

            try
            {
                _logger.LogDebug("Buzz provider attempt: {Provider}", provider.Name);
                return await provider.GenerateAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                failures.Add($"{provider.Name}: {ex.Message}");
                _logger.LogWarning(ex, "Buzz provider failed: {Provider}", provider.Name);
            }
        }

        _logger.LogError(
            "All Buzz providers failed. Attempts: {Attempts}",
            string.Join(" | ", failures));
        throw new InvalidOperationException(
            $"All Buzz providers failed. Attempts: {string.Join(" | ", failures)}");
    }

    private IReadOnlyList<string> BuildProviderPreference()
    {
        var ordered = new List<string>();

        if (!string.IsNullOrWhiteSpace(_options.DefaultProviderName))
        {
            ordered.Add(_options.DefaultProviderName);
        }

        if (_options.EnableProviderFailover)
        {
            ordered.AddRange(_options.ProviderFailoverOrder);
            ordered.AddRange(_providers.Keys);
        }

        return ordered
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}
