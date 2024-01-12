using AffiliateService.Api.Tests.Integration.Support;
using AffiliateService.Api.Tests.Integration.Support.Models;
using Flurl.Http;
using NUnit.Framework;

namespace AffiliateService.Api.Tests.Integration.StepDefinitions
{
    [Binding]
    public sealed class AffiliateControllerStepDefinitions
    {
        private readonly ScenarioExecutionContext _context;
        private readonly FeatureContext _featureContext;

        public AffiliateControllerStepDefinitions(
            ScenarioExecutionContext context, 
            FeatureContext featureContext)
        {
            _context = context;
            _featureContext = featureContext;
        }

        [When(@"I POST an Affiliate to '([^']*)'")]
        public async Task WhenIPOSTAnAffiliateTo(string resourceSegment)
        {
            var responseTask = _context.AffiliateServiceClient
                .WithPayload(new InsertUpdateAffiliate($"Affiliate {Guid.NewGuid():N}"))
                .WithSegment(resourceSegment)
                .PostAsync();

            var uniqueId = await responseTask
                .ReceiveJson<Guid>();

            var header = responseTask.Result.Headers.FirstOrDefault("Location");

            _context.Data.Add("ResponseStatusCode", responseTask.Result.StatusCode);
            _context.Data.Add("InsertAffiliateResponseUniqueId", uniqueId);
            _context.Data.Add("LocationHeader", header);
            _context.Data.Add("ResourceUriSegment", resourceSegment);
        }

        [When(@"I try to fetch it from '([^']*)'")]
        public async Task WhenITryToFetchItFrom(string segmentWithUniqueId)
        {
            var uniqueId = _context.Data["InsertAffiliateResponseUniqueId"] as Guid?;
            var filledSegment = segmentWithUniqueId.Replace("{uniqueid}", uniqueId.Value.ToString());

            var responseTask = _context.AffiliateServiceClient
                .WithSegment(filledSegment)
                .GetAsync();

            var affiliate = await responseTask
                .ReceiveJson<Affiliate>();

            _context.Data["ResponseStatusCode"] = responseTask.Result.StatusCode;
            _context.Data.Add("FetchedAffiliate", affiliate);
        }

        [Given(@"an Affiliate has just been inserted")]
        public async Task GivenAnAffiliateHasJustBeenInserted()
        {
            await WhenIPOSTAnAffiliateTo("/api/v1/Affiliate");
        }

        [Then(@"and the response is a an Affiliate with the same UniqueId")]
        public void ThenAndTheResponseIsAAnAffiliateWithTheSameUniqueId()
        {
            var affiliate = _context.Data["FetchedAffiliate"] as Affiliate;
            var uniqueId = _context.Data["InsertAffiliateResponseUniqueId"] as Guid?;

            Assert.AreEqual(uniqueId.Value, affiliate.UniqueId);
        }
    }
}
