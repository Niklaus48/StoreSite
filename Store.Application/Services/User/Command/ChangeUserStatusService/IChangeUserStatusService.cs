using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.User.Command.ChangeUserStatusService
{
    public interface IChangeUserStatusService
    {
        ResultDto Excute(long UserId);
    }

}
