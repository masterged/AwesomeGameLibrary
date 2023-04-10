using AwesomeGameLibrary.Application.Features.Games.Queries;
using AwesomeGameLibrary.Domain.Database.Entities;
using AwesomeGameLibrary.Web.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeGameLibrary.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class GameLibraryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GameLibraryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<Game>>> GetAllGames()
    {
        var result = await _mediator.Send(new GamesQuery());
        return result.Match(
            Ok,
            errors => 
                ValidationProblem(errors.ToModelState()));
    }
}