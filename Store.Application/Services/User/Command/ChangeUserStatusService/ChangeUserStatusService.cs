using Store.Application.Interface.Context;
using Store.Common.Dto;

namespace Store.Application.Services.User.Command.ChangeUserStatusService
{
    public class ChangeUserStatusService : IChangeUserStatusService
    {
        private readonly IDataBaseContext _context;

        public ChangeUserStatusService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Excute(long UserId)
        {
            var user = _context.Users.Find(UserId);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر مورد نظر پیدا نشد",
                };
            }

            user.IsActive = !user.IsActive;
            _context.SaveChanges();

            string statusChange = user.IsActive? "فعال":"غیرفعال";
            return new ResultDto
            {
                IsSuccess = true,
                Message = $"کاربر مورد نظر با موفقیت {statusChange}شد",
            };
        }
    }

}
