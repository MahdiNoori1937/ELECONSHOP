using System.Security.Claims;
using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.Queries;
using ELECON.Domain.Interface.IUserRepository;
using MediatR;

namespace ELECON.Application.Feature.User.Handler;

public class UserGetClaimsHandler:IRequestHandler<GetUserClaimsQuery,List<Claim>>
{
    private readonly IUserRepository _userRepository;

    public UserGetClaimsHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<Claim>> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
    {
        Domain.Entities.User.User User = await _userRepository.FindByEmailOrNumberAsync(request.Input);
        List<Claim> Claims = new()
        {
            new Claim("Id", User.Id.ToString()),
            new Claim("RoleId", User.RoleId.ToString()),
        };
        return Claims;
    }
}