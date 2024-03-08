using AwesomeGameLibrary.Application.Features.Games.Queries;
using AwesomeGameLibrary.DAL.Contexts;
using AwesomeGameLibrary.Domain.Database.Entities;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AwesomeGameLibrary.Application.Features.Games;

public class GamesQueryHandler : IRequestHandler<GamesQuery, ErrorOr<IReadOnlyCollection<Game>>>
{
    private readonly AwesomeDbContext _dbContext;

    public GamesQueryHandler(AwesomeDbContext dbContext) => _dbContext = dbContext;

    public async Task<ErrorOr<IReadOnlyCollection<Game>>> Handle(GamesQuery request, CancellationToken cancellationToken) 
        => await _dbContext.Games.ToArrayAsync(cancellationToken: cancellationToken);
}