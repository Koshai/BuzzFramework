# Getting Started

This guide shows how to run Buzz with Blazor, configure providers, and start using components.

## 1) Add Services

In your app startup (`Program.cs`), register Buzz.

**Option A: Bind from configuration** (recommended)

```csharp
builder.Services.AddBuzzFramework(builder.Configuration);
```

Options are bound from the `Buzz` section in `appsettings.json`. You can override in code:

```csharp
builder.Services.AddBuzzFramework(builder.Configuration, o => o.DefaultProviderName = "ollama");
```

**Option B: Configure in code**

```csharp
builder.Services.AddBuzzFramework(options =>
{
    options.DefaultProviderName = "openai";
    options.ProviderFailoverOrder = ["openai", "ollama", "mock"];
});
```

## 2) Configure Providers

Buzz supports OpenAI (online), Ollama (offline/local), and mock fallback. Use the provider extensions for easy registration:

```csharp
// Config-based (from appsettings Buzz:OpenAI, Buzz:Ollama)
if (!string.IsNullOrWhiteSpace(config["Buzz:OpenAI:ApiKey"] ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY")))
    builder.Services.AddBuzzOpenAI(config);
if (!string.IsNullOrWhiteSpace(config["Buzz:Ollama:BaseUrl"]))
    builder.Services.AddBuzzOllama(config);
builder.Services.AddBuzzMock();  // Always register as fallback
```

### OpenAI

- Set environment variable: `$env:OPENAI_API_KEY = "sk-..."`
- Or set `Buzz:OpenAI:ApiKey` in app settings.
- Set `DefaultProvider` to `"openai"` and ensure OpenAI is registered before Ollama to try it first.
- Model defaults to `gpt-4o-mini`; override via `Buzz:OpenAI:Model`.
- Control cost with `Buzz:OpenAI:MaxOutputTokens` and `Buzz:OpenAI:Temperature`.
- Requires `BuzzBlazor.Provider.OpenAI` package.

### Ollama

- Run Ollama locally.
- Configure in appsettings: `Buzz:Ollama:BaseUrl` (default: `http://localhost:11434/api/`), `Buzz:Ollama:Model` (any installed model, default: `llama3.2:latest`).
- Use any model you have pulled: `llama3.2:latest`, `mistral`, `phi`, etc. The model name is passed directly to Ollama.
- Requires `BuzzBlazor.Provider.Ollama` package.

## Provider prioritization

The first provider to try is `DefaultProviderName` (config: `Buzz:DefaultProvider`). If that provider fails or is not registered, Buzz uses `ProviderFailoverOrder` to try the next provider.

- Set `DefaultProvider` to `"openai"` to prefer OpenAI when an API key is available.
- Set to `"ollama"` to prefer local Ollama.
- Set to `"mock"` for development without AI.
- The configured default is only used if that provider is registered. Otherwise, Buzz picks the first available provider (openai → ollama → mock).

## 3) Core Buzz Settings

When using configuration binding, these map to the `Buzz` section. Settings are loaded into `BuzzOptions`.

- `DefaultProvider` (config) / `DefaultProviderName` (code): first provider to try.
- `ProviderFailoverOrder`: fallback order.
- `EnableProviderFailover`: fallback on provider failure.
- `EnableAiSuggestions`: turns AI enrichment on/off.
- `AiMinInputLength`: minimum input length before AI calls.
- `AiMaxLocalResultsBeforeSkip`: skip AI when local suggestions are strong.
- `AiCooldownSeconds`: minimum time between AI calls per textbox context.
- `AiCacheTtlSeconds`: cache AI suggestion results.
- `AiMaxPromptCharacters`: hard cap for composed AI context/prompt size.
- `AiMaxUserInputCharacters`: max input chars sent for AI suggestions.
- `AiMaxRequestsPerDay`: hard cap on total AI generation requests per UTC day (`0` disables limit).
- `AiBudgetExceededBehavior`: when budget is exceeded (`throw` or `fallback-mock`).
- `EnableSharedCaseMemory`: enables shared subject memory.
- `SharedCaseMemoryMaxEntriesPerSubject`: cap per subject.

### Cost-Safe Defaults (Recommended)

- Keep `AiMaxPromptCharacters` around `1200-2000` for daily use.
- Keep `AiMaxUserInputCharacters` around `250-500`.
- Set `Buzz:OpenAI:MaxOutputTokens` around `120-300` for short assistant outputs.
- Prefer lower `Temperature` (for example `0.1-0.3`) to reduce verbose drift.
- Set `AiMaxRequestsPerDay` to a practical budget (for example `200-500`) during development.
- Use `AiBudgetExceededBehavior: "fallback-mock"` in development to keep UI functional when budget is reached.

## 4) Run the Sample

```powershell
dotnet run --project Buzz.Samples
```

Open:

- `/`
- `/textbox-demo`
- `/accessibility-checklist`

Try the `Resolution Category` field on `/textbox-demo` to validate `BuzzComboBox` ranking behavior.
Try the `Escalation Level` field on `/textbox-demo` to validate `BuzzSelectBox` ranking behavior.
Try the `Require MFA reset` checkbox on `/textbox-demo` to validate `BuzzCheckBox` recommendation behavior.
Try the `Case Summary` card on `/textbox-demo` to validate `BuzzCard` AI summarization behavior.
Try the `Resolution Path` radio group on `/textbox-demo` to validate `BuzzRadioGroup` ranking behavior.
Try the `Follow-up Date` field on `/textbox-demo` to validate `BuzzDatePicker` quick-suggestion behavior.
Try the `Case Review Dialog` on `/textbox-demo` to validate `BuzzModal` AI insight behavior.
Try the `Save Notification` section on `/textbox-demo` to validate `BuzzToast` behavior.
Try the `Field Help` section on `/textbox-demo` to validate `BuzzTooltip` behavior.
Try the `Troubleshooting Playbook` section on `/textbox-demo` to validate `BuzzAccordion` behavior.
Try the `Resolution Views` section on `/textbox-demo` to validate `BuzzTabs` behavior.
Try the `Guided Workflow` section on `/textbox-demo` to validate `BuzzStepper` behavior.
Try the `Activity Trail` section on `/textbox-demo` to validate `BuzzTimeline` behavior.
Try the `Quick Actions` section on `/textbox-demo` to validate `BuzzCommandPalette` behavior.
Try the `Case Snapshot Table` section on `/textbox-demo` to validate `BuzzSmartTable` behavior.
Try the `Pre-Submit Coach` section on `/textbox-demo` to validate `BuzzFormAssistant` behavior.
Try the `Case Workflow Board` section on `/textbox-demo` to validate `BuzzKanbanBoard` behavior.
Try the `Action Controls`, `Status Pills`, `Content Separation`, and `Flexible Layout` sections on `/textbox-demo` to validate `BuzzButton`, `BuzzBadge`, `BuzzDivider`, and `BuzzStack`.
Try the `Inline Alerts`, `Identity Avatars`, `Completion Indicator`, and `Loading Placeholders` sections on `/textbox-demo` to validate `BuzzAlert`, `BuzzAvatar`, `BuzzProgress`, and `BuzzSkeleton`.
Try the `Filter Tags`, `Snapshot Metrics`, `Empty View Prompt`, and `Guided Checklist` sections on `/textbox-demo` to validate `BuzzChip`, `BuzzStatCard`, `BuzzEmptyState`, and `BuzzList`.
Try the `Navigation Trail`, `Data Pagination`, `Analytics Snapshot`, and `Outcome Banner` sections on `/textbox-demo` to validate `BuzzBreadcrumb`, `BuzzPagination`, `BuzzDataPanel`, and `BuzzResultBanner`.
Try the `Readable Snippets` and `AI Code Workspace` sections on `/textbox-demo` to validate `BuzzCodeBlock` and `BuzzCodeEditor`.
Try the `Action Menu`, `Upload Intake`, `Date Window Selector`, and `Product Footer` sections on `/textbox-demo` to validate `BuzzDropdownMenu`, `BuzzFileUpload`, `BuzzDateRangePicker`, and `BuzzFooter`.
Try the `Sidebar Navigation`, `Slide-out Drawer`, `Confirmation Prompt`, and `Toast Hub` sections on `/textbox-demo` to validate `BuzzSideNav`, `BuzzDrawer`, `BuzzConfirmDialog`, and `BuzzToastCenter`.
Try the `Marketing Hero`, `Pricing Layout`, `Feature Carousel`, and `Auth Container` sections on `/textbox-demo` to validate `BuzzHero`, `BuzzPricingTable`, `BuzzCarousel`, and `BuzzAuthShell`.
Use `/accessibility-checklist` as a release gate to track keyboard, semantics, and visual accessibility checks.

Run baseline component tests:

```powershell
dotnet test Buzz.Blazor.Tests
```

## 5) Debug Tips

In development config:

- set logging level for `Buzz.Blazor` to `Debug`
- watch provider attempts, failover logs, and suggestion logs in terminal
- to verify OpenAI: set `DefaultProvider` to `"openai"`, set `OPENAI_API_KEY`, and ensure OpenAI is registered (AddBuzzOpenAI). Buzz will try OpenAI first; check logs for "Buzz provider attempt: openai"

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

Dark styles are opt-in. Light mode is default unless you explicitly apply `data-buzz-theme="dark"` on a parent container.
