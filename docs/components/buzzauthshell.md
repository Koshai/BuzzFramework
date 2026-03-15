# BuzzAuthShell

`BuzzAuthShell` provides a reusable, branded authentication container for sign-in flows.

## Basic usage

```razor
<BuzzAuthShell Title="Welcome back" Subtitle="Sign in to continue.">
    <ChildContent>
        <!-- inputs -->
    </ChildContent>
    <Actions>
        <BuzzButton Text="Sign in" Variant="primary" />
    </Actions>
</BuzzAuthShell>
```

## Parameters and effects

- `Title` / `Subtitle`: auth form context text.
- `ChildContent`: main form inputs.
- `Actions`: action/footer area.
