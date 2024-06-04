using MediatR;

namespace ETicaretAPI.Application.Features.Queries.User.GetRolesToUser
{
    public class GetRolesToUserQueryRequest:IRequest<GetRolesToUserQueryResponse>
    {
        public string userId { get; set; }
    }
}