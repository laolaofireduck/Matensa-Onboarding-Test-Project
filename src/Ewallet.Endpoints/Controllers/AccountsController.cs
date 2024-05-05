using Ewallet.Core.Application.Accounts.Transfer;
using Ewallet.Core.Application.Authentication.Login;
using Ewallet.Endpoints.Contracts.Accounts;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Endpoints.Controllers;

[Route("accounts")]
[Authorize(AuthenticationSchemes = "Bearer")]

public class AccountsController : ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AccountsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Trasfer(TransferRequest request)
    {
        var query = _mapper.Map<TransferCommand>(request);
        var result = await _mediator.Send(query);


        return result.Match(
            result => Ok(_mapper.Map<TransferResponse>(result)),
            errors => Problem(errors));
    }
}
