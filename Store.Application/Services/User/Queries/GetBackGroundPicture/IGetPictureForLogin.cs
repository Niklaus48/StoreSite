using Store.Application.Interface.Context;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.User.Queries.GetBackGroundPicture
{
    public interface IGetPictureForLogin
    {
        ResultDto<string> Excute();
    }

    public class GetPictureForLogin : IGetPictureForLogin
    {
        private readonly IDataBaseContext _context;

        public GetPictureForLogin(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<string> Excute()
        {
            Random rnd = new Random();
            int random = rnd.Next(0,_context.LoginBackGrounds.Count());
            var picture = _context.LoginBackGrounds.OrderBy(p => Guid.NewGuid())
                .Skip(random)
                .FirstOrDefault();

            return new ResultDto<string>
            {
                Data = picture.Address,
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
