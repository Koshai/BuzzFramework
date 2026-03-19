# BuzzBlazor

BuzzBlazor is an open-source Blazor component ecosystem focused on reusable UI, accessibility, theming, and practical AI-assisted UX patterns.

## Packages

NuGet package IDs:

- `BuzzBlazor` (UI components)
- `BuzzBlazor.Core` (core contracts and abstractions)
- `BuzzBlazor.Provider.OpenAI` (OpenAI provider integration)
- `BuzzBlazor.Provider.Ollama` (Ollama provider integration)

Current preview line:

- `0.1.0-preview.2`

## Quick Start (NuGet Consumer)

1. Create a Blazor app (`Blazor Web App` in Visual Studio, or `dotnet new blazor`).
2. Install packages:

```powershell
dotnet add package BuzzBlazor --version 0.1.0-preview.2
dotnet add package BuzzBlazor.Core --version 0.1.0-preview.2
dotnet add package BuzzBlazor.Provider.OpenAI --version 0.1.0-preview.2
```

3. Register the framework in `Program.cs`:

```csharp
builder.Services.AddBuzzFramework(options =>
{
    options.DefaultProviderName = "openai";
    options.ProviderFailoverOrder = ["openai", "ollama", "mock"];
    options.EnableAiSuggestions = true;
});
```

4. Use components in a page:

```razor
<BuzzTextBox Label="Issue Summary" @bind-InputText="_summary" />
<BuzzCard Title="Case Overview" EnableAiSummary="true" SourceText="@_summary" />
```

## AI Context Bootstrap (Cold Start)

BuzzBlazor supports seed-first AI behavior so components can provide useful output from initial deployment:

- Seed knowledge loaded at startup
- Component subject keys (for example `AiContextSubject`) to route context
- Live user memory progressively taking precedence over seed defaults

Sample seed file path:

```text
Buzz.Samples/seed/buzz-seed-knowledge.json
```

## Sample Site (Temporary Hosting)

- Repository: [https://github.com/Koshai/BuzzFramework](https://github.com/Koshai/BuzzFramework)
- Temporary URL target: `https://buzzblazor-samples.onrender.com`

Render deploy (free tier):

1. Connect repository to Render.
2. Choose Blueprint deploy.
3. Render uses `render.yaml` and `Buzz.Samples/Dockerfile`.
4. After first deploy, update the URL here if Render assigns a different domain.
