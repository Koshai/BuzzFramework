# Buzz Framework

Buzz Framework is a Blazor component ecosystem focused on reusable UI, accessibility, theming, and practical AI-assisted UX patterns.

## Projects

- `Buzz.Core` - provider abstractions and request/response contracts
- `Buzz.Blazor` - reusable component library
- `Buzz.Provider.OpenAI` - OpenAI provider implementation
- `Buzz.Provider.Ollama` - Ollama provider implementation
- `Buzz.Samples` - reference site and in-app developer guide

## Local NuGet Packages

Package IDs:

- `Buzz.Framework.Core`
- `Buzz.Framework.Blazor`
- `Buzz.Framework.Provider.OpenAI`
- `Buzz.Framework.Provider.Ollama`

Build local packages:

```powershell
tools\pack-local.cmd Release 0.1.0-preview.2
```

Packages are produced in:

```text
.artifacts/nuget
```

Register/update local feed:

```powershell
tools\add-local-feed.cmd
```

## AI Context Bootstrap (Cold Start)

Buzz supports seed-first AI behavior so components can provide relevant output on day one:

- seed knowledge JSON loaded at startup
- component subject keys (for example `AiContextSubject`) to route context
- live user memory progressively takes precedence over baseline seed context

Sample seed file path in `Buzz.Samples`:

```text
Buzz.Samples/seed/buzz-seed-knowledge.json
```

## GitHub Readiness

This repository includes a root `.gitignore` with common .NET, IDE, package, and local artifact exclusions (including `.artifacts/` and local config patterns).
