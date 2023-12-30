using Store.Application.Interface.Context;
using Store.Common;
using Store.Common.Dto;

namespace Store.Application.Services.User.Queries.GetUser
{
    public class GetUserListService : IGetUserListService
    {
        private readonly IDataBaseContext _context;

        public GetUserListService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetUserDto> Excute(RequestGetUserDto request)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(p => p.FullName.Contains(request.SearchKey) || p.Email.Contains(request.SearchKey));
            }
            int rowsCount = 0;
            var userList = users.Select(p => new GetUserDto
            {
                Id = p.Id,
                FullName = p.FullName,
                Email = p.Email,
                IsActive = p.IsActive,
            }).ToList();

            return new ResultDto<ResultGetUserDto>
            {
                Data = new ResultGetUserDto
                {
                    Users = userList,
                    Rows = rowsCount,
                },
                IsSuccess = true,
                Message = "",
            };
        }
    }
}
