using Microsoft.AspNetCore.Mvc.Filters;

namespace AffiliateService.Api.Filters
{
    public class ErrorHandlingFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            //WE can immplement as a filter but it seems like as a middlewared is preffered by the community
            throw new NotImplementedException();
        }
    }
}
