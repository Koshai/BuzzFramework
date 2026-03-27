using Buzz.Samples.Components;
using Buzz.Blazor;
using Buzz.Blazor.Services;
using Buzz.Core;
using Buzz.Provider.Ollama;
using Buzz.Provider.OpenAI;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var openAiApiKey = config["Buzz:OpenAI:ApiKey"] ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");
var hasOpenAi = !string.IsNullOrWhiteSpace(openAiApiKey);
var ollamaBaseUrl = config["Buzz:Ollama:BaseUrl"];
var hasOllama = !string.IsNullOrWhiteSpace(ollamaBaseUrl);
var configuredDefaultProvider = config["Buzz:DefaultProvider"];
// Only use configured default if that provider is available; otherwise pick first available.
var defaultProvider = ResolveDefaultProvider(configuredDefaultProvider, hasOpenAi, hasOllama);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBuzzFramework(config, options =>
{
    options.DefaultProviderName = defaultProvider;
});

if (hasOpenAi)
{
    builder.Services.AddBuzzOpenAI(config);
}

if (hasOllama)
{
    builder.Services.AddBuzzOllama(config);
}

builder.Services.AddBuzzMock();

var app = builder.Build();

if (config.GetValue("Buzz:EnableSeedKnowledgeWarmupOnStartup", true))
{
    using var scope = app.Services.CreateScope();
    var seedStore = scope.ServiceProvider.GetRequiredService<IBuzzSeedKnowledgeStore>();
    await seedStore.WarmupAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static string ResolveDefaultProvider(string? configured, bool hasOpenAi, bool hasOllama)
{
    var available = configured switch
    {
        "openai" when hasOpenAi => "openai",
        "ollama" when hasOllama => "ollama",
        "mock" => "mock",
        _ => null
    };
    return available ?? (hasOpenAi ? "openai" : hasOllama ? "ollama" : "mock");
}
