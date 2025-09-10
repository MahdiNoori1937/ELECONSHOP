using ELECON.Application.Feature.User.DTOs;
using ELECON.Application.Feature.User.Queries;
using ELECON.Domain.Interface.IUserRepository;
using MediatR;

namespace ELECON.Application.Feature.User.Handler;

public class UserDetailHandler:IRequestHandler<GetUserDetailQuery,UserDetailDto>
{
    private readonly IUserRepository _userRepository;

    public UserDetailHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserDetailDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserDetailById(request.Id);
    }
}