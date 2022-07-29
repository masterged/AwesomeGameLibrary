using AwesomeGameLibrary.Application.Features.Games.Queries;
using AwesomeGameLibrary.DAL.Contexts;
using AwesomeGameLibrary.Domain.Database.Entities;
using ErrorOr;
using MediatR;

namespace AwesomeGameLibrary.Application.Features.Games;

public class GamesAddHandler : IRequestHandler<GamesAddCommand, ErrorOr<Game>>
{
    private readonly AwesomeDbContext _awesomeDbContext;

    public GamesAddHandler(AwesomeDbContext awesomeDbContext) => _awesomeDbContext = awesomeDbContext;


    public async Task<ErrorOr<Game>> Handle(GamesAddCommand request, CancellationToken cancellationToken)
    {
        var entity = _awesomeDbContext.Games.Add(request.Game);
        await _awesomeDbContext.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }
}