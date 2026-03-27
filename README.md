# BuzzBlazor

BuzzBlazor is a Blazor component framework focused on reusable UI, accessibility, theming, and practical AI-assisted UX patterns.

- Website: [https://www.buzzblazor.com](https://www.buzzblazor.com)
- Repository: [https://github.com/Koshai/BuzzFramework](https://github.com/Koshai/BuzzFramework)

## Packages

NuGet package IDs:

- `BuzzBlazor`
- `BuzzBlazor.Core`
- `BuzzBlazor.Provider.OpenAI`
- `BuzzBlazor.Provider.Ollama`

Current preview line:

- `0.1.0-preview.3`

## Installation

1. Create a Blazor app (`Blazor Web App` in Visual Studio or `dotnet new blazor`).
2. Install packages:

```powershell
dotnet add package BuzzBlazor --version 0.1.0-preview.3
dotnet add package BuzzBlazor.Core --version 0.1.0-preview.3
dotnet add package BuzzBlazor.Provider.OpenAI --version 0.1.0-preview.3
dotnet add package BuzzBlazor.Provider.Ollama --version 0.1.0-preview.3
```

## Basic Setup

Register Buzz and providers in `Program.cs`:

```csharp
builder.Services.AddBuzzFramework(builder.Configuration, options =>
{
    options.DefaultProviderName = "openai";
    options.ProviderFailoverOrder = ["openai", "ollama", "mock"];

    // Cost controls
    options.AiMaxPromptCharacters = 1800;
    options.AiMaxUserInputCharacters = 350;
    options.AiMaxRequestsPerDay = 400;
    options.AiBudgetExceededBehavior = "fallback-mock";
});

if (!string.IsNullOrWhiteSpace(builder.Configuration["Buzz:OpenAI:ApiKey"]
    ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY")))
{
    builder.Services.AddBuzzOpenAI(builder.Configuration);
}

if (!string.IsNullOrWhiteSpace(builder.Configuration["Buzz:Ollama:BaseUrl"]))
{
    builder.Services.AddBuzzOllama(builder.Configuration);
}

builder.Services.AddBuzzMock();
```

## Provider Behavior

Provider selection order is:

1. `Buzz:DefaultProvider`
2. `Buzz:ProviderFailoverOrder`
3. any remaining registered providers

This makes it easy to prefer OpenAI or Ollama while keeping a safe fallback.

## Cost Controls (Recommended)

For daily development, use these practical defaults:

- `AiMaxPromptCharacters`: `1200-2000`
- `AiMaxUserInputCharacters`: `250-500`
- `AiMaxRequestsPerDay`: `200-500`
- `AiBudgetExceededBehavior`: `fallback-mock`
- `Buzz:OpenAI:MaxOutputTokens`: `120-300`
- `Buzz:OpenAI:Temperature`: `0.1-0.3`

## Learn More

- Website docs and examples: [https://www.buzzblazor.com](https://www.buzzblazor.com)
- In-repo docs: `docs/`
- Sample app source: `Buzz.Samples/`

