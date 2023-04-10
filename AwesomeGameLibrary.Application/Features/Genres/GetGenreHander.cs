using System.ComponentModel.Design;
using AwesomeGameLibrary.Application.Features.Audit.Command;
using AwesomeGameLibrary.Application.Features.Genres.Queries;
using AwesomeGameLibrary.DAL.Contexts;
using AwesomeGameLibrary.Domain.Audit;
using AwesomeGameLibrary.Domain.Database.Entities;
using MediatR;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace AwesomeGameLibrary.Application.Features.Genres;

public class GetGenreHander : IRequestHandler<GenresGetByIdQuery,ErrorOr<Genre>>
{
    private readonly AwesomeDbContext _awesomeDbContext;
    private readonly IMediator _mediator;

    public GetGenreHander(AwesomeDbContext awesomeDbContext, IMediator mediator)
    {
        _awesomeDbContext = awesomeDbContext;
        _mediator = mediator;
    }
    public async Task<ErrorOr<Genre>> Handle(GenresGetByIdQuery request, CancellationToken cancellationToken)
    {
        Genre? result;
        await _mediator.Publish(new FancyAuditCommand<GenreEvent>(new GenreEvent()), cancellationToken);
        
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