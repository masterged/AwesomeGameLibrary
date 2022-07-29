using System.Reflection;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace AwesomeGameLibrary.Application.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : IErrorOr
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => this._validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany<ValidationResult, ValidationFailure>(result => result.Errors)
                .ToArray();
            
            if (failures.Any())
            {
                var errors = failures.Select(x => Error.Validation(
                    code: x.PropertyName,
                    description: x.ErrorMessage)).ToList();
                
                var response = (TResponse?)typeof(TResponse)
                    .GetMethod(
                        name: nameof(ErrorOr<object>.From),
                        bindingAttr: BindingFlags.Static | BindingFlags.Public,
                        types: new[] { typeof(List<Error>) })?
                    .Invoke(null, new object?[] { errors })!;

                return response ?? throw new ValidationException(failures);
            }

            return await next();
        }
    }
}