using AwesomeGameLibrary.Domain.Database.Entities;
using ErrorOr;
using MediatR;

namespace AwesomeGameLibrary.Application.Features.Games.Queries;

public record GamesQuery : IRequest<ErrorOr<IReadOnlyCollection<Game>>>;