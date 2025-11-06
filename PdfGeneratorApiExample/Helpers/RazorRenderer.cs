using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace PdfGeneratorApiExample.Helpers
{
    public class RazorRenderer : IAsyncDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly ILoggerFactory _loggerFactory;
        private readonly HtmlRenderer _htmlRenderer;
        public RazorRenderer()
        {
            // Build all the dependencies for the HtmlRenderer
            var services = new ServiceCollection();
            services.AddLogging();
            _serviceProvider = services.BuildServiceProvider();
            _loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            _htmlRenderer = new HtmlRenderer(_serviceProvider, _loggerFactory);
        }

        // Dispose the services and DI container we created
        public async ValueTask DisposeAsync()
        {
            await _htmlRenderer.DisposeAsync();
            _loggerFactory.Dispose();
            await _serviceProvider.DisposeAsync();
        }

        // The other public methods are identical
        public Task<string> RenderComponentAsync<T>() where T : IComponent
            => RenderComponentAsync<T>(ParameterView.Empty);

        public Task<string> RenderComponentAsync<T>(Dictionary<string, object?> dictionary) where T : IComponent
            => RenderComponentAsync<T>(ParameterView.FromDictionary(dictionary));

        private Task<string> RenderComponentAsync<T>(ParameterView parameters) where T : IComponent
        {
            return _htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var output = await _htmlRenderer.RenderComponentAsync<T>(parameters);
                return output.ToHtmlString();
            });
        }
    }
}
