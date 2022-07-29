using AwesomeGameLibrary.Application.Features.Genres.Queries;
using AwesomeGameLibrary.DAL.Contexts;
using AwesomeGameLibrary.Domain.Database.Entities;
using ErrorOr;
using MediatR;

namespace AwesomeGameLibrary.Application.Features.Genres;

public class GenreAddHandler : IRequestHandler<GenresAddCommand,ErrorOr<Genre>>
{
    private readonly AwesomeDbContext _awesomeDbContext;

    public GenreAddHandler(AwesomeDbContext awesomeDbContext) => _awesomeDbContext = awesomeDbContext;

    public async Task<ErrorOr<Genre>> Handle(GenresAddCommand request, CancellationToken cancellationToken)
    {
        var entity = _awesomeDbContext.Genres.Add(request.Genre);
        await _awesomeDbContext.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }
}