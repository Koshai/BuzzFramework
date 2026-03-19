using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace Buzz.Samples.E2E.Tests;

[Collection(TutorialE2eCollection.CollectionName)]
public sealed class TutorialFlowTests(TutorialE2eFixture fixture)
{
    [Fact]
    public async Task MedicalTutorialRoute_ShouldRenderCoreSections()
    {
        var page = await fixture.NewPageAsync();
        await page.GotoAsync($"{fixture.SiteBaseUrl}/tutorial-medical", new PageGotoOptions
        {
            WaitUntil = WaitUntilState.NetworkIdle
        });

        await Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Build a Medical Triage Page with Buzz Components + AI" }))
            .ToBeVisibleAsync();
        await Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Live Medical Triage Demo" }))
            .ToBeVisibleAsync();
        await Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Final Step: Full Code (Copy-Paste)" }))
            .ToBeVisibleAsync();
    }

    [Fact]
    public async Task HomePage_ShouldExposeThemeControls()
    {
        var page = await fixture.NewPageAsync();
        await page.GotoAsync($"{fixture.SiteBaseUrl}/", new PageGotoOptions
        {
            WaitUntil = WaitUntilState.NetworkIdle
        });

        await Expect(page.Locator("#buzz-theme-select")).ToBeVisibleAsync();
        await Expect(page.Locator(".buzz-theme-apply-button")).ToBeVisibleAsync();
    }
}
