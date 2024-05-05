using Ewallet.Core.Application.Authentication;
using Ewallet.Core.Domain.Users;
using Ewallet.Endpoints.Contracts.Authentication;
using Mapster;
namespace Ewallet.Endpoints.Mappings;


public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User)
            .Map(dest => dest.Id, src => UserId.Create(src.User.Id.Value));

    }
}