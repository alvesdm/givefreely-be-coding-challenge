using System.Text.Json.Serialization;

namespace AffiliateService.Infrastructure
{
    public class HttpModelValidationErrors
    {
        public HttpModelValidationErrors()
        {
            Errors = new Dictionary<string, List<string>>();
        }

        [JsonPropertyName("validation")]
        public Dictionary<string, List<string>> Errors { get; private set; }

        /// <summary>
        /// Add new property with errors
        /// </summary>
        /// <param name="property"></param>
        /// <param name="errors"></param>
        public void Add(string property, List<string> errors)
        {
            Errors.Add(property, errors);
        }

        /// <summary>
        /// add a new error to an existing property
        /// </summary>
        /// <param name="property"></param>
        /// <param name="error"></param>
        public void AddError(string property, string error)
        {
            var item = Errors.First(e => e.Key == property);
            item.Value.Add(error);
        }
    }
}
