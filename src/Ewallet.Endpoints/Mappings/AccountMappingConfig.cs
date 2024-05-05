using Ewallet.Core.Application.Accounts.Transfer;
using Ewallet.Core.Application.Authentication;
using Ewallet.Core.Domain.Users;
using Ewallet.Endpoints.Contracts.Accounts;
using Ewallet.Endpoints.Contracts.Authentication;
using Mapster;

namespace Ewallet.Endpoints.Mappings;

internal class AccountMappingConfigIRegister: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<TransferResult, TransferResponse>()
        //    .Map(dest => dest., src => UserId.Create(src.User.Id.Value));
        
        //config.NewConfig<TransferRequest, TransferCommand>()
        //    .Map(dest => dest.RecieverId, src => UserId.Create(src.RecieverId));
    }
}