using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using Razor.Templating.Core;
using System.Threading.Tasks;

namespace PuppeteerSharpDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppController : ControllerBase
    {
        const string HtmlPath = @"Templates\ExampleTemplate.cshtml";

        [HttpGet("to-pdf")]
        public async Task<IActionResult> Get(bool openInNewTab = true)
        {
            string html = await RenderHtml(HtmlPath);

            var pdfBytes = await ConvertHtmlToPdf(html);

            return File(pdfBytes, "application/pdf", openInNewTab ? string.Empty : "example.pdf");
        }

        private static async Task<string> RenderHtml(string htmlPath)
        {
            return await RazorTemplateEngine.RenderAsync(htmlPath);
        }

        private static async Task<byte[]> ConvertHtmlToPdf(string html)
        {
            var browser = await InitializeBrowser();

            var page = await CreatePageAndSetContent(html, browser);

            PdfOptions pdfOptions = new()
            {
                Format = PaperFormat.A4,
                DisplayHeaderFooter = true,
                MarginOptions = new MarginOptions
                {
                    Top = "20px",
                    Right = "20px",
                    Bottom = "40px",
                    Left = "20px"
                },
                PrintBackground = true,
            };

            return await page.PdfDataAsync(pdfOptions);
        }

        private static async Task<Browser> InitializeBrowser()
        {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
            });

            return browser;
        }

        private static async Task<Page> CreatePageAndSetContent(string html, Browser browser)
        {
            var page = await browser.NewPageAsync();
            await page.SetContentAsync(html);

            return page;
        }
    }
}

