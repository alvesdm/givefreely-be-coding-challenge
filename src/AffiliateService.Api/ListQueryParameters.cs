using System.ComponentModel.DataAnnotations;

namespace AffiliateService.Api
{
    public class ListQueryParameters
    {
        [MinLength(3)]
        public string? criteria { get; set; } //Nullable to allow empty one for no filtering
    }
}