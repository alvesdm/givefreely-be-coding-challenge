namespace AffiliateService.Infrastructure
{
    internal static class ApplicationConstants
    {
        public static string AffiliateServiceDatabasePath => Path.Join(AppDomain.CurrentDomain.BaseDirectory, "data", "AffiliateService.db");

        public static string AffiliateServiceDatabase => "AffiliateServiceDb";
    }
}
