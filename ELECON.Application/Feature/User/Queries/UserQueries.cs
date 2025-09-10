using System.Security.Claims;
using ELECON.Application.Feature.User.DTOs;
using MediatR;

namespace ELECON.Application.Feature.User.Queries;

public class GetUserClaimsQuery : IRequest<List<Claim>>
{
    public string Input { get; set; }

    public GetUserClaimsQuery(string input)
    {
        Input = input;
    }
}

public class GetUserDetailQuery : IRequest<UserDetailDto>
{

    public int Id { get; set; }

    public GetUserDetailQuery(int id)
    {
        Id = id;
    }
}
