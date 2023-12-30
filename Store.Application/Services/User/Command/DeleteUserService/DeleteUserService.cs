using Store.Application.Interface.Context;
using Store.Common.Dto;

namespace Store.Application.Services.User.Command.DeleteUserService
{
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IDataBaseContext _context;

        public DeleteUserService(IDataBaseContext context)
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
                    Message = "کاربر پیدا نشد",
                };
            }

            user.RemoveTime = DateTime.Now;
            user.IsRemoved = true;
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت حذف شد.",
            };
        }
    }
}
