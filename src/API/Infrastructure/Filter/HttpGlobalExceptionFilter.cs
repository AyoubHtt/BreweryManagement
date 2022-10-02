namespace API.Infrastructure.Filter;

public class HttpGlobalExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// Constructor
    /// </summary>
    public HttpGlobalExceptionFilter()
    {
    }

    /// <summary>
    /// Auto call in case of exception
    /// </summary>
    /// <param name="context"></param>
    public void OnException(ExceptionContext context)
    {
        if (IsSameOrSubclass(context.Exception.GetType()))
        {
            var domainException = (DomainException)context.Exception;

            var problemDetails = new ValidationExceptionDetails()
            {
                Instance = context.HttpContext.Request.Path,
                Status = domainException.StatusCode,
                Detail = "Please refer to the errors property for additional details.",
                Message = domainException.Message,
                ErrorCodes = domainException.ErrorCodes
            };

            if (domainException.Errors != null)
            {
                foreach (var error in domainException.Errors)
                {
                    problemDetails.Errors?.Add(error);
                }
            }

            context.Result = new BadRequestObjectResult(problemDetails);
            context.HttpContext.Response.StatusCode = domainException.StatusCode;
        }
    }

    private static bool IsSameOrSubclass(Type potentialDescendant)
    {
        return potentialDescendant.IsSubclassOf(typeof(DomainException))
               || potentialDescendant == typeof(DomainException);
    }
}
