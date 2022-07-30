using System.Net.Mime;
using AwesomeGameLibrary.DAL.Contexts;
using AwesomeGameLibrary.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeGameLibrary.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class GameLibraryController : ControllerBase
{
    private readonly AwesomeDbContext _dbContext;

    public GameLibraryController(AwesomeDbContext dbContext) => _dbContext = dbContext;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyCollection<Game>> GetAllGames()
    {
        var result = _dbContext.Games.AsAsyncEnumerable();
        return Ok(result);
    }
}