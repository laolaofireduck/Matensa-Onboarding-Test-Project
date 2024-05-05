using Ewallet.Core.Application.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using Ewallet.Endpoints.Contracts.Users;
using Ewallet.Core.Application.Users.Update;
using Ewallet.Core.Application.Users.List;
using Ewallet.Core.Application.Users.Delete;
using Ewallet.Core.Application.Accounts.AddToInitial;
using Ewallet.Core.Domain.Users;
using Ewallet.Endpoints.Contracts.Accounts;


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

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ListUserRequest request)
    {
        var command = _mapper.Map<ListUserQuery>(request);
        var listUserResult = await _mediator.Send(command);

        return listUserResult.Match(
            listResult => Ok(_mapper.Map<List<UserResponse>>(listResult)),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        var command = _mapper.Map<CreateUserCommand>(request);
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            createResult => Created($"api/users/{createResult.Id.Value}", _mapper.Map<UserResponse>(createResult)),
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

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            updateResult => Ok(_mapper.Map<UserResponse>(updateResult)),
            errors => Problem(errors));
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteUserCommand(
           Id: id
           );

        var DeleteResult = await _mediator.Send(command);

        return DeleteResult.Match(
            deleteResult => Ok(),
            errors => Problem(errors));
    }

    [HttpPut("{id:Guid}/account/balance")]
    public async Task<IActionResult> AddToInititalBalance(Guid id, AddToInititalBalanceRequest request)
    {
        var command = new AddToInititalBalanceCommand(
            UserId.Create(id),
            request.Amount
            );

        var Result = await _mediator.Send(command);

        return Result.Match(
            result => Ok(_mapper.Map<AccountResponse>(result)),
            errors => Problem(errors));
    }
}
