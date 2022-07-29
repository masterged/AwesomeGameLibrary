using AwesomeGameLibrary.Domain.Database.Entities;
using ErrorOr;
using MediatR;

namespace AwesomeGameLibrary.Application.Features.Genres.Queries;

public record GenresGetByIdQuery(int Id) : IRequest<ErrorOr<Genre>>;