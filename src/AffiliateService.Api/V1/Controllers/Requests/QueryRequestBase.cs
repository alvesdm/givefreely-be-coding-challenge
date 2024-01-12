using MediatR;

namespace AffiliateService.Api.V1.Controllers.Requests
{
    public class QueryRequestBase<T> : PagedQuery<T>
    {
        public QueryRequestBase(string criteria, int page, int pageSize) : base(page, pageSize)
        {
            Criteria = criteria;
        }

        public string Criteria { get; }
    }

    public class PagedQuery<T> : IRequest<PagedResult<T>>
    {
        public PagedQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; }
        public int PageSize { get; }
    }
}