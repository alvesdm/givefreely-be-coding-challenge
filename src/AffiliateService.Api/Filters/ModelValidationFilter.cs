using AffiliateService.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AffiliateService.Api.Filters
{
    public class ModelValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var e = new HttpModelValidationErrors();

                context.ModelState.Keys.ToList().ForEach(k =>
                {
                    e.Add(k, context.ModelState[k]!.Errors.Select(r => r.ErrorMessage).ToList());
                });

                throw new BadRequestHttpException("One or more validation errors occurred.", e);
            }

            await next();
        }
    }
}
