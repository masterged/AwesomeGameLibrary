using AwesomeGameLibrary.Application.Features.Genres.Queries;
using FluentValidation;

namespace AwesomeGameLibrary.Application.Features.Genres.Validation;

public class GenresGetByIdValidator : AbstractValidator<GenresGetByIdQuery>
{
    public GenresGetByIdValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}