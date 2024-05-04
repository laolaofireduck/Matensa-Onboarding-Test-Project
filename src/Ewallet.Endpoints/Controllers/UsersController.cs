using Ewallet.Core.Application.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using Ewallet.Endpoints.Contracts.Users;


namespace Ewallet.Endpoints.Controllers;

[Route("api")]
public class UsersController :ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public UsersController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpPost("users")]
    public async Task<IActionResult> Register(CreateUserRequest request)
    {
        var command = _mapper.Map<CreateUserCommand>(request);
        var authenticationResult = await _mediator.Send(command);

        return authenticationResult.Match(
            authenticationResult => Ok(),
            errors => Problem(errors));
    }

}
