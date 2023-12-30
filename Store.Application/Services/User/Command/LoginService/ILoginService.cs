using Microsoft.EntityFrameworkCore;
using Store.Application.Interface.Context;
using Store.Common.Dto;
using Store.Common.HashPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.User.Command.LoginService
{
    public interface ILoginService
    {
        ResultDto<ResultSigninDto> Excute(RequestLoginDto request);
    }

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

            var HashPassword = PasswordHasher.Execute(request.Password);
            bool IsCorrectPassword = HashPassword == user.Password;

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

    public class RequestLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ResultSigninDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Roles { get; set; }
    }
}
