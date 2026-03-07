using Buzz.Samples.Components;
using Buzz.Blazor;
using Buzz.Core;
using Buzz.Provider.Ollama;
using Buzz.Provider.OpenAI;
using Buzz.Samples.Services;

var builder = WebApplication.CreateBuilder(args);
var openAiApiKey = builder.Configuration["Buzz:OpenAI:ApiKey"]
    ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");
var openAiModel = builder.Configuration["Buzz:OpenAI:Model"] ?? "gpt-4o-mini";
var ollamaBaseUrl = builder.Configuration["Buzz:Ollama:BaseUrl"] ?? "http://localhost:11434/api/";
var ollamaModel = builder.Configuration["Buzz:Ollama:Model"] ?? "llama3.1:8b";
var configuredDefaultProvider = builder.Configuration["Buzz:DefaultProvider"];
var configuredFailoverOrder = builder.Configuration
    .GetSection("Buzz:ProviderFailoverOrder")
    .Get<string[]>() ?? ["openai", "ollama", "mock"];
var enableAiSuggestions = builder.Configuration.GetValue("Buzz:EnableAiSuggestions", false);
var aiMinInputLength = builder.Configuration.GetValue("Buzz:AiMinInputLength", 12);
var aiMaxLocalResultsBeforeSkip = builder.Configuration.GetValue("Buzz:AiMaxLocalResultsBeforeSkip", 2);
var aiCooldownSeconds = builder.Configuration.GetValue("Buzz:AiCooldownSeconds", 10);
var aiCacheTtlSeconds = builder.Configuration.GetValue("Buzz:AiCacheTtlSeconds", 180);
var enableSharedCaseMemory = builder.Configuration.GetValue("Buzz:EnableSharedCaseMemory", true);
var sharedCaseMemoryMaxEntriesPerSubject = builder.Configuration.GetValue("Buzz:SharedCaseMemoryMaxEntriesPerSubject", 2000);
var hasOpenAi = !string.IsNullOrWhiteSpace(openAiApiKey);
var hasOllama = !string.IsNullOrWhiteSpace(ollamaBaseUrl);
var defaultProvider = !string.IsNullOrWhiteSpace(configuredDefaultProvider)
    ? configuredDefaultProvider
    : hasOpenAi ? "openai" : hasOllama ? "ollama" : "mock";

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBuzzFramework(options =>
{
    options.DefaultProviderName = defaultProvider;
    options.ProviderFailoverOrder = configuredFailoverOrder;
    options.EnableAiSuggestions = enableAiSuggestions;
    options.AiMinInputLength = aiMinInputLength;
    options.AiMaxLocalResultsBeforeSkip = aiMaxLocalResultsBeforeSkip;
    options.AiCooldownSeconds = aiCooldownSeconds;
    options.AiCacheTtlSeconds = aiCacheTtlSeconds;
    options.EnableSharedCaseMemory = enableSharedCaseMemory;
    options.SharedCaseMemoryMaxEntriesPerSubject = sharedCaseMemoryMaxEntriesPerSubject;
});
builder.Services.AddHttpClient("buzz-openai", client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/");
    client.Timeout = TimeSpan.FromSeconds(30);
});
builder.Services.AddHttpClient("buzz-ollama", client =>
{
    client.BaseAddress = new Uri(ollamaBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});

if (hasOpenAi)
{
    builder.Services.AddScoped<IBuzzProvider>(serviceProvider =>
    {
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var client = httpClientFactory.CreateClient("buzz-openai");
        var options = new OpenAiBuzzOptions
        {
            ApiKey = openAiApiKey!,
            Model = openAiModel
        };

        return new OpenAiBuzzProvider(client, options);
    });
}

if (hasOllama)
{
    builder.Services.AddScoped<IBuzzProvider>(serviceProvider =>
    {
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var client = httpClientFactory.CreateClient("buzz-ollama");
        var options = new OllamaBuzzOptions
        {
            Model = ollamaModel
        };

        return new OllamaBuzzProvider(client, options);
    });
}

builder.Services.AddScoped<IBuzzProvider, MockBuzzProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
