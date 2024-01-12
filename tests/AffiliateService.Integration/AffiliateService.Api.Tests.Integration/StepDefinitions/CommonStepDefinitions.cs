using AffiliateService.Api.Tests.Integration.Support;
using NUnit.Framework;

namespace AffiliateService.Api.Tests.Integration.StepDefinitions
{
    [Binding]
    public sealed class CommonStepDefinitions
    {
        private readonly ScenarioExecutionContext _context;
        private readonly FeatureContext _featureContext;

        public CommonStepDefinitions(
            ScenarioExecutionContext context, 
            FeatureContext featureContext)
        {
            _context = context;
            _featureContext = featureContext;
        }

        [Then(@"the response status code is (.*)")]
        public void TheResponseStatusCodeIs(int statusCode) //
        {
            var responseStatusCode = _context.Data["ResponseStatusCode"];

            Assert.AreEqual(statusCode, responseStatusCode);
        }

        [Then(@"and the response has the resource uri in the Location header")]
        public void AndTheResponseHasTheResourceUriInTheLocationHeader() //
        {
            var header = _context.Data["LocationHeader"] as string;
            var segment = _context.Data["ResourceUriSegment"] as string;

            Assert.IsTrue(header.Contains(segment));
        }
    }
}
