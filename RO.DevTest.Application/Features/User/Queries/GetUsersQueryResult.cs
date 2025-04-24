using RO.DevTest.Application.Features.User.DTO;

namespace RO.DevTest.Application.Features.User.Queries
{
    public class GetUsersQueryResult
    {
        public List<UserDTO> Users { get; set; } = new();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}