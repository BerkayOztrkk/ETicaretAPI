using MediatR;

namespace ETicaretAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryRequest:IRequest<GetAllUsersQueryResponse>
    {
        public int page { get; set; } = 0;
        public int size { get; set; } = 5;
    }
}