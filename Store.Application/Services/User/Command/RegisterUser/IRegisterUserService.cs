using Store.Common.Dto;
using System.ComponentModel.DataAnnotations;

namespace Store.Application.Services.User.Command.RegisterUser
{
    public interface IRegisterUserService
    {
        ResultDto<ResultRegisterUserDto> Excute(RequestRegisterUser request);
    }
}
