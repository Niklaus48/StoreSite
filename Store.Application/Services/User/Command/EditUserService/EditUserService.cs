using Store.Application.Interface.Context;
using Store.Common.Dto;

namespace Store.Application.Services.User.Command.EditUserService
{
    public class EditUserService : IEditUserService
    {
        private readonly IDataBaseContext _context;

        public EditUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Excute(long UserId, string FullName)
        {
            var user = _context.Users.Find(UserId);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                };
            }

            user.FullName = FullName;
            user.UpdateTime = DateTime.Now;
            _context.SaveChanges();

            return new ResultDto { IsSuccess = true, Message = "عملیات با موفقیت انجام شد." };
        }
    }
}
