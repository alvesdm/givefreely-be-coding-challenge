using AffiliateService.Domain;

namespace AffiliateService.Infrastructure.Repository
{
    internal class EFEntityUpdateHelper
    {
        internal static void Update<T>(List<T> current, List<T> affiliate)
            where T : IUnique
        {
            //remove the no longer existing ones
            current
                .ToList() //avoid changing the original loop
                .ForEach(c => {
                    if (!affiliate.Any(e => e.UniqueId.Equals(c.UniqueId)))
                    {
                        current.Remove(c);
                    }
                });

            //include the new ones
            affiliate
                .ForEach(c => {
                    if (!current.Any(e => e.UniqueId.Equals(c.UniqueId)))
                    {
                        current.Add(c);
                    }
                });
        }
    }
}