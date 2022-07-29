using AwesomeGameLibrary.Domain.Database.Entities;
using ErrorOr;
using MediatR;

namespace AwesomeGameLibrary.Application.Features.Genres.Queries;

public record GenresAddCommand(Genre Genre) : IRequest<ErrorOr<Genre>>;