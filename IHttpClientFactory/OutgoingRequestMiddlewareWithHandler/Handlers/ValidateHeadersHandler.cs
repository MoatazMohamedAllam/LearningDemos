using System.Net;
using System.Net.Http;

namespace OutgoingRequestMiddlewareWithHandler.Handlers
{
    public class ValidateHeadersHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if there is no api-key header supplied , short-circuiting the handler pipeline to avoid unnecessary http call
            if (!request.Headers.Contains("API-Key"))
            {
                var x = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("You must supply an API key header called X-API-KEY")
                };

                return Task.FromResult(x);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
