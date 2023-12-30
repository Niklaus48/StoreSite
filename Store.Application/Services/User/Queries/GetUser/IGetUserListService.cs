using Store.Common.Dto;

namespace Store.Application.Services.User.Queries.GetUser
{
    public interface IGetUserListService
    {
        ResultDto<ResultGetUserDto> Excute(RequestGetUserDto request);
    }
}
