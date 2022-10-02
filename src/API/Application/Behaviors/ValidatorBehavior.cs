using Domain.SystemConstants;

namespace API.Application.Behaviors;

/// <summary>
/// Validate all sent commands if it has a declared validation class, continue to handler if it valid or handle exception
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest>[] _validators;

    /// <summary>
    /// Behavior validation constructor
    /// </summary>
    /// <param name="validators"></param>
    public ValidatorBehavior(IValidator<TRequest>[] validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    /// <summary>
    /// Behavior validation handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    /// <exception cref="DomainException"></exception>
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            var errors = new Dictionary<string, string[]>()
                {
                    {ValidationConstants.FluentValidation,failures.Select(c=>c.ErrorMessage).ToArray() }
                };

            throw new DomainException($"Command Validation Errors for type {typeof(TRequest).Name}", errors, null, (int)HttpStatusCode.BadRequest);
        }

        return next();
    }
}
