using AwesomeGameLibrary.Application.Features.Genres.Queries;
using AwesomeGameLibrary.Domain.Database.Entities;
using AwesomeGameLibrary.Web.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AwesomeGameLibrary.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenreController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add([FromBody] Genre genre)
    {
        var result = await _mediator.Send(new GenresAddCommand(genre));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GenresGetByIdQuery(id));
        return result.Match(
            Ok,
            errors =>
            {
                if (errors.All(x => x.Type == ErrorType.Validation))
                    return ValidationProblem(errors.ToModelState());

                if (errors.All(x => x.Type == ErrorType.NotFound))
                    return NotFound(errors.FirstOrDefault());

                return Problem();

            });
    }
}