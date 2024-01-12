using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace AffiliateService.Api.Tests.Integration.Support
{
    public class ScenarioExecutionContext
    {
        private readonly IConfigurationRoot _configuration;

        public ScenarioExecutionContext()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.test.json", optional: true, reloadOnChange: true);

            _configuration = configBuilder.Build();

            InitializeHttpClient();
        }

        private void InitializeHttpClient()
        {
            var baseUri = _configuration.GetValue<Uri>("AffiliateService:BaseUrl");
            AffiliateServiceClient = new AffiliateServiceClient(baseUri);
        }

        public Dictionary<string, object> Data = new Dictionary<string, object>();

        public AffiliateServiceClient AffiliateServiceClient { get; set; }
    }
}
