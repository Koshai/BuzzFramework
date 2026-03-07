# Getting Started

This guide shows how to run Buzz with Blazor, configure providers, and start using components.

## 1) Add Services

In your app startup (`Program.cs`), register Buzz:

```csharp
builder.Services.AddBuzzFramework(options =>
{
    options.DefaultProviderName = "openai";
    options.ProviderFailoverOrder = ["openai", "ollama", "mock"];
});
```

## 2) Configure Providers

Buzz supports OpenAI (online), Ollama (offline/local), and mock fallback.

### OpenAI

- Set environment variable:
  - PowerShell: `$env:OPENAI_API_KEY = "sk-..."`
- Or set `Buzz:OpenAI:ApiKey` in app settings.

### Ollama

- Run Ollama locally.
- Configure:
  - `Buzz:Ollama:BaseUrl` (default: `http://localhost:11434/api/`)
  - `Buzz:Ollama:Model` (default: `llama3.1:8b`)

## 3) Core Buzz Settings

Settings are loaded into `BuzzOptions`.

- `DefaultProviderName`: first provider to try.
- `ProviderFailoverOrder`: fallback order.
- `EnableProviderFailover`: fallback on provider failure.
- `EnableAiSuggestions`: turns AI enrichment on/off.
- `AiMinInputLength`: minimum input length before AI calls.
- `AiMaxLocalResultsBeforeSkip`: skip AI when local suggestions are strong.
- `AiCooldownSeconds`: minimum time between AI calls per textbox context.
- `AiCacheTtlSeconds`: cache AI suggestion results.
- `EnableSharedCaseMemory`: enables shared subject memory.
- `SharedCaseMemoryMaxEntriesPerSubject`: cap per subject.

## 4) Run the Sample

```powershell
dotnet run --project Buzz.Samples
```

Open:

- `/`
- `/textbox-demo`

## 5) Debug Tips

In development config:

- set logging level for `Buzz.Blazor` to `Debug`
- watch provider attempts, failover logs, and suggestion logs in terminal

## 6) Design Tokens (2026-ready)

Buzz components use CSS variables so you can keep one modern design language across current and future components.

Starter token names:

- `--buzz-surface`, `--buzz-surface-dark`
- `--buzz-text-primary`, `--buzz-text-muted`
- `--buzz-border`, `--buzz-border-strong`
- `--buzz-accent`, `--buzz-focus-ring`
- `--buzz-radius-md`, `--buzz-radius-lg`
- `--buzz-shadow-sm`, `--buzz-shadow-md`

Define these in your app-level stylesheet (`wwwroot/app.css`) and every Buzz component can inherit the same visual system.
