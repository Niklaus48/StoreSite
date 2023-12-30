
using Store.Application.Interface.Context;
using Store.Common.Dto;
using Store.Common.HashPassword;
using Store.Domain.Entities;
using System.Text.RegularExpressions;

namespace Store.Application.Services.User.Command.RegisterUser
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDataBaseContext _context;

        public RegisterUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultRegisterUserDto> Excute(RequestRegisterUser request)
        {
            //try
            //{
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ResultDto<ResultRegisterUserDto>
                    {
                        Data = new ResultRegisterUserDto
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "لطفا ایمیل را وارد کنید",
                    };
                }
                if (string.IsNullOrWhiteSpace(request.FullName))
                {
                    return new ResultDto<ResultRegisterUserDto>
                    {
                        Data = new ResultRegisterUserDto
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "لطفا نام را وارد کنید",
                    };
                }
                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ResultDto<ResultRegisterUserDto>
                    {
                        Data = new ResultRegisterUserDto
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "لطفا پسورد را وارد کنید",
                    };
                }
                if (request.Password != request.RePassword)
                {
                    return new ResultDto<ResultRegisterUserDto>
                    {
                        Data = new ResultRegisterUserDto
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "پسورد و تکرار آن برابر نیستند",
                    };
                }

                string emailRegex = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
                var match = Regex.Match(request.Email, emailRegex);

                if (!match.Success)
                {
                    return new ResultDto<ResultRegisterUserDto>
                    {
                        Data = new ResultRegisterUserDto
                        {
                            UserId = 0
                        },
                        IsSuccess = false,
                        Message = "آدرس ایمیل نامعتبر است",
                    };
                }

                Domain.Entities.User user = new Domain.Entities.User
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    Password = PasswordHasher.Execute(request.Password),
                    IsActive = true,
                };
                List<UserInRole> userInRoles = new List<UserInRole>();

                foreach (var item in request.Roles)
                {
                    var roles = _context.Roles.Find(item.Id);
                    userInRoles.Add(new UserInRole
                    {
                        Role = roles,
                        RoleId = roles.Id,
                        User = user,
                        UserId = user.Id,
                    });
                }
                user.userInRoles = userInRoles;
                _context.Users.Add(user);
                _context.SaveChanges();

                return new ResultDto<ResultRegisterUserDto>
                {
                    Data = new ResultRegisterUserDto
                    {
                        UserId = user.Id,
                    },
                    IsSuccess = true,
                    Message = "کاربر با موفقیت ثبت نام شد",
                };
            //}
            //catch (Exception)
            //{
            //    return new ResultDto<ResultRegisterUserDto>
            //    {
            //        Data = new ResultRegisterUserDto
            //        {
            //            UserId = 0,
            //        },
            //        IsSuccess = false,
            //        Message = "عملیات با شکست مواجه شد",
            //    };
            //}
        }
    }
}
