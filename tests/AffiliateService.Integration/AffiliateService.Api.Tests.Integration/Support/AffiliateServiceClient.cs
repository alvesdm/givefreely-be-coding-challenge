using Flurl;
using Flurl.Http;

namespace AffiliateService.Api.Tests.Integration.Support
{
    public class AffiliateServiceClient
    {
        private readonly Uri _baseUri;
        private object _payload;
        private string _segment;

        public AffiliateServiceClient(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public Task<IFlurlResponse> PostAsync()
        {
            return _baseUri
                .AppendPathSegment(_segment)
                .PostJsonAsync(_payload);
        }

        public Task<IFlurlResponse> GetAsync()
        {
            return _baseUri
                .AppendPathSegment(_segment)
                .GetAsync();
        }

        internal AffiliateServiceClient WithPayload<T>(T payload)
        {
            _payload = payload;

            return this;
        }

        internal AffiliateServiceClient WithSegment(string segment)
        {
            _segment = segment;

            return this;
        }
    }
}