using Bunit;
using Buzz.Blazor;

namespace Buzz.Blazor.Tests;

public sealed class ComponentSmokeTests : BunitContext
{
    [Fact]
    public void BuzzButton_InvokesClickHandler_WhenEnabled()
    {
        var clickCount = 0;

        var cut = Render<BuzzButton>(parameters => parameters
            .Add(parameter => parameter.Text, "Save")
            .Add(parameter => parameter.OnClick, () => clickCount++));

        cut.Find("button").Click();

        Assert.Equal(1, clickCount);
    }

    [Fact]
    public void BuzzButton_DoesNotInvokeClickHandler_WhenLoading()
    {
        var clickCount = 0;

        var cut = Render<BuzzButton>(parameters => parameters
            .Add(parameter => parameter.Text, "Save")
            .Add(parameter => parameter.Loading, true)
            .Add(parameter => parameter.OnClick, () => clickCount++));

        cut.Find("button").Click();

        Assert.Equal(0, clickCount);
    }

    [Fact]
    public void BuzzAlert_Dismiss_UpdatesVisibilityBinding()
    {
        var visible = true;

        var cut = Render<BuzzAlert>(parameters => parameters
            .Add(parameter => parameter.Title, "Attention")
            .Add(parameter => parameter.Dismissible, true)
            .Add(parameter => parameter.IsVisible, visible)
            .Add(parameter => parameter.IsVisibleChanged, value => visible = value));

        cut.Find("button.buzz-alert-close").Click();

        Assert.False(visible);
    }

    [Fact]
    public void BuzzPagination_Next_EmitsNextPage()
    {
        var currentPage = 2;

        var cut = Render<BuzzPagination>(parameters => parameters
            .Add(parameter => parameter.CurrentPage, currentPage)
            .Add(parameter => parameter.TotalPages, 8)
            .Add(parameter => parameter.CurrentPageChanged, value => currentPage = value));

        cut.FindAll("button")
            .Single(button => string.Equals(button.TextContent.Trim(), "Next", StringComparison.Ordinal))
            .Click();

        Assert.Equal(3, currentPage);
    }
}
