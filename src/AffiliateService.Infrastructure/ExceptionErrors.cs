namespace AffiliateService.Infrastructure
{
    public class BadRequestHttpException : Exception
    {
        public BadRequestHttpException(string message, HttpModelValidationErrors errors) : base(message)
        {
            Errors = errors;
        }

        public HttpModelValidationErrors Errors { get; }
    }

    public class NotFoundHttpException : Exception
    {
        public NotFoundHttpException(string message) : base(message)
        { }
        public NotFoundHttpException(string message, Exception ex) : base(message, ex)
        { }
    }

    public class NotImplementedHttpException : Exception
    {
        public NotImplementedHttpException(string message, Exception ex) : base(message, ex)
        { }
    }

    public class UnauthorizedAccessHttpException : Exception
    {
        public UnauthorizedAccessHttpException(string message, Exception ex) : base(message, ex)
        { }
    }
}
