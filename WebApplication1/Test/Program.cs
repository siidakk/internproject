namespace WebApplication1.Test;
using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

public class MyTests
{
    [Fact]
    public async Task Homepage_Should_Have_Correct_Title()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true // for localhost HTTPS
        });

        var page = await context.NewPageAsync();

        await page.GotoAsync("https://localhost:44348");

        // ✅ Assert the page title
        var title = await page.TitleAsync();
        Assert.Equal("Home Page - WebApplication1", title);

        // ✅ Assert content on the page
        var content = await page.InnerTextAsync("h1");
        Assert.Contains("Welcome", content);
    }
}
