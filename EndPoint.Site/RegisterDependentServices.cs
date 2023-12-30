using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interface.Context;
using Store.Application.Services.User.Command.ChangeUserStatusService;
using Store.Application.Services.User.Command.DeleteUserService;
using Store.Application.Services.User.Command.EditUserService;
using Store.Application.Services.User.Command.RegisterUser;
using Store.Application.Services.User.Queries.GetRole;
using Store.Application.Services.User.Queries.GetUser;
using Store.Presistance.Context;

namespace EndPoint.Site
{
    public static class RegisterDependentServices
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
            builder.Services.AddScoped<IGetUserListService, GetUserListService>();
            builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();
            builder.Services.AddScoped<IGetRoleService, GetRoleService>();
            builder.Services.AddScoped<IChangeUserStatusService, ChangeUserStatusService>();
            builder.Services.AddScoped<IDeleteUserService, DeleteUserService>();
            builder.Services.AddScoped<IEditUserService, EditUserService>();

            string ConectionString = "Data Source=.;Initial Catalog=StoreDb; Integrated Security=True;";
            builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(ConectionString));
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            });

            return builder;
        }
    }
}
