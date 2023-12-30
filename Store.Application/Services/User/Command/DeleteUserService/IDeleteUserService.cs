using Store.Common.Dto;

namespace Store.Application.Services.User.Command.DeleteUserService
{
    public interface IDeleteUserService
    {
        ResultDto Excute(long UserId);
    }
}
