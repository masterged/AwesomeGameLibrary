using AwesomeGameLibrary.Application.Features.Genres.Queries;
using FluentValidation;

namespace AwesomeGameLibrary.Application.Features.Genres.Validation;

public class GenreAddValidator : AbstractValidator<GenresAddCommand>
{
    public GenreAddValidator()
    {
        RuleFor(x => x.Genre.Name).NotEmpty();
        RuleFor(x => x.Genre.Name).NotNull();
    }
}