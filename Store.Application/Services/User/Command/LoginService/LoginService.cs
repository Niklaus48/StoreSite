using Microsoft.EntityFrameworkCore;
using Store.Application.Interface.Context;
using Store.Common.Dto;
using Store.Common.HashPassword;

namespace Store.Application.Services.User.Command.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly IDataBaseContext _context;

        public LoginService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultSigninDto> Excute(RequestLoginDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return new ResultDto<ResultSigninDto>
                {
                    Data = new ResultSigninDto
                    {

                    },
                    IsSuccess = false,
                    Message = "لطفا ایمیل و پسورد را وارد کنید",
                };
            }

            var user = _context.Users
                .Include(p => p.userInRoles)
                .ThenInclude(p => p.Role)
                .Where(p => p.Email.Equals(request.Email) && p.IsActive == true)
                .FirstOrDefault();

            if (user == null)
            {
                return new ResultDto<ResultSigninDto>
                {
                    Data = new ResultSigninDto
                    {

                    },
                    IsSuccess = false,
                    Message = "حساب کاربری با این ایمیل موجود نمی باشد",
                };
            }

            PasswordHasher hasher = new PasswordHasher();
            string PasswordHash = hasher.HashPassword(request.Password);
            bool IsCorrectPassword = hasher.VerifyPassword(PasswordHash,request.Password);

            if (!IsCorrectPassword)
            {
                return new ResultDto<ResultSigninDto>
                {
                    Data = new ResultSigninDto
                    {

                    },
                    IsSuccess = false,
                    Message = "پسورد وارد شده صحیح نیست",

                };
            }

            var roles = "";
            foreach (var item in user.userInRoles)
            {
                roles += $"{item.Role.Name}";
            }

            return new ResultDto<ResultSigninDto>
            {
                Data = new ResultSigninDto
                {
                    UserId = user.Id,
                    FullName = user.FullName,
                    Roles = roles,
                },
                IsSuccess = true,
                Message = "ورورد موفق"
            };
        }
    }
}
