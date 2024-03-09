using AwesomeGameLibrary.Application.Features.Genres.Queries;
using AwesomeGameLibrary.DAL.Contexts;
using AwesomeGameLibrary.Domain.Database.Entities;
using MediatR;
using ErrorOr;

namespace AwesomeGameLibrary.Application.Features.Genres;

public class GetGenreHandler : IRequestHandler<GenresGetByIdQuery,ErrorOr<Genre>>
{
    private readonly AwesomeDbContext _awesomeDbContext;

    public GetGenreHandler(AwesomeDbContext awesomeDbContext)
    {
        _awesomeDbContext = awesomeDbContext;
    }
    public async Task<ErrorOr<Genre>> Handle(GenresGetByIdQuery request, CancellationToken cancellationToken)
    {
        Genre? result;
        try
        {
            result = await _awesomeDbContext.Genres.FindAsync(request.Id, cancellationToken);
        }
        catch (Exception e)
        {
            return Error.Unexpected(e.HResult.ToString(),e.Message);
        }
        if (result == null) return Error.NotFound();
        return result!;
    }
}