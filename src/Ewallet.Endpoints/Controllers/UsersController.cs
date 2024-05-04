using Ewallet.Core.Application.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using Ewallet.Endpoints.Contracts.Users;
using Ewallet.Core.Application.Users.Update;


namespace Ewallet.Endpoints.Controllers;

[Route("api/users")]
public class UsersController : ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public UsersController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        var command = _mapper.Map<CreateUserCommand>(request);
        var authenticationResult = await _mediator.Send(command);

        return authenticationResult.Match(
            authenticationResult => Created($"api/users/{authenticationResult.Id.Value}",_mapper.Map<UserResponse>(authenticationResult)),
            errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var command = new UpdateUserCommand(
            Id: id,
            FirstName: request.FirstName,
            LastName: request.LastName,
            DOB: request.DOB,
            PhoneNumber: request.PhoneNumber,
            Email: request.Email,
            Password: request.Password
            );

        var authenticationResult = await _mediator.Send(command);

        return authenticationResult.Match(
            authenticationResult => Ok(_mapper.Map<UserResponse>(authenticationResult)),
            errors => Problem(errors));
    }

}
