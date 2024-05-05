using Ewallet.Core.Application.Users;
using Ewallet.Core.Application.Users.Create;
using Ewallet.Core.Domain.Users;
using Ewallet.Endpoints.Contracts.Users;
using Mapster;

namespace Ewallet.Endpoints.Mappings;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserRequest, CreateUserCommand>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.DOB, src => src.DOB);
        
        config.NewConfig<UserResult, UserResponse>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.Id, src => UserId.Create(src.Id.Value));
    }
}
