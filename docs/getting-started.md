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
- Requires `BuzzBlazor.Provider.OpenAI` package.

### Ollama

- Run Ollama locally.
- Configure in appsettings: `Buzz:Ollama:BaseUrl` (default: `http://localhost:11434/api/`), `Buzz:Ollama:Model` (default: `llama3.1:8b`).
- Requires `BuzzBlazor.Provider.Ollama` package.

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
- `EnableSharedCaseMemory`: enables shared subject memory.
- `SharedCaseMemoryMaxEntriesPerSubject`: cap per subject.

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
