using System.Diagnostics;

namespace OutgoingRequestMiddlewareWithHandler.Handlers
{
    public class TimingHandler : DelegatingHandler
    {
        private readonly ILogger<TimingHandler> _logger;

        public TimingHandler(ILogger<TimingHandler> logger)
        {
            _logger = logger;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var sw = new Stopwatch();

            _logger.LogInformation("Starting request");

            var response = base.SendAsync(request, cancellationToken);

            _logger.LogInformation($"Finished request in {sw.ElapsedMilliseconds}ms");

            return response;
        }
    }
}
