# Changelog

## 0.1.0-preview.3

### Added

- Provider registration extensions for easier setup:
  - `AddBuzzOpenAI(...)`
  - `AddBuzzOllama(...)`
  - `AddBuzzMock()`
- AI cost-control options:
  - `AiMaxPromptCharacters`
  - `AiMaxUserInputCharacters`
  - `AiMaxRequestsPerDay`
  - `AiBudgetExceededBehavior` (`throw` or `fallback-mock`)
- OpenAI generation controls:
  - `Buzz:OpenAI:MaxOutputTokens`
  - `Buzz:OpenAI:Temperature`

### Changed

- Configuration-first setup with `AddBuzzFramework(builder.Configuration, ...)`.
- Default sample Ollama model moved to `llama3.2:latest` (fully configurable).
- Provider selection now respects availability more clearly.

### Documentation

- Reworked package README for installation, usage, and links.
- Replaced temporary sample-site wording with official site: [https://www.buzzblazor.com](https://www.buzzblazor.com)
- Updated `docs/getting-started.md` with provider priority and cost-control guidance.
- Updated Buzz.Samples guide snippets (`Home`, `Getting Started`, `Installation`, `Developer Guide`) to reflect latest setup and multi-provider install.

