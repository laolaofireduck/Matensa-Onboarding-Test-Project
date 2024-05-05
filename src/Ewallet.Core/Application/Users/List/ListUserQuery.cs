using ErrorOr;
using MediatR;

namespace Ewallet.Core.Application.Users.List;

public record ListUserQuery(int? Skip, int? Take) : IRequest<ErrorOr<IEnumerable<UserResult>>>;
