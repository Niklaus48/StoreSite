using Store.Application.Interface.Context;
using Store.Common.Dto;

namespace Store.Application.Services.User.Queries.GetRole
{
    public class GetRoleService : IGetRoleService
    {
        private readonly IDataBaseContext _context;

        public GetRoleService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<RolesDto>> Excute()
        {

            var roles = _context.Roles.Select(p => new RolesDto
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();

            return new ResultDto<List<RolesDto>>()
            {
                Data = roles,
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
