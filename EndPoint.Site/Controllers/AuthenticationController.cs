using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.User.Command.LoginService;
using Store.Application.Services.User.Command.RegisterUser;
using Store.Application.Services.User.Queries.GetBackGroundPicture;
using System.Security.Claims;

namespace EndPoint.Site.Controllers
{
    //jhbjbkj
    public class AuthenticationController : Controller
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly ILoginService _loginService;
        private readonly IGetPictureForLogin _getPicture;

        public AuthenticationController(IRegisterUserService registerUserService, ILoginService loginService, IGetPictureForLogin getPicture)
        {
            _registerUserService = registerUserService;
            _loginService = loginService;
            _getPicture = getPicture;
        }

        [HttpGet]
        public IActionResult Regiser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string FullName, string Email, string Password, string RePassword)
        {
            var signupResult = _registerUserService.Excute(new RequestRegisterUser
            {
                FullName = FullName,
                Email = Email,
                Password = Password,
                RePassword = RePassword,
                Roles = new List<RolesInRegisterUserDto>
                {
                    new RolesInRegisterUserDto
                    {
                        Id = 3,
                    }
                }
            });

            if (!signupResult.IsSuccess == true)
            {
                return Json(signupResult);
            }

            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, signupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Name,FullName),
                new Claim (ClaimTypes.Email,Email),
                new Claim (ClaimTypes.Role,"Customer"),
            };

            var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true,
            };

            HttpContext.SignInAsync(principal, properties);

            return Json(signupResult);
        }

        public IActionResult Login(string ReturnUrl = "/")
        {
            ViewBag.url = ReturnUrl;
            return View(_getPicture.Excute());
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            var loginUser = _loginService.Excute(new RequestLoginDto 
            { 
                Email = Email, Password = Password,
            });

            if(loginUser.IsSuccess == true)
            {
                var Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,loginUser.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Name,loginUser.Data.FullName),
                    new Claim(ClaimTypes.Email,Email),
                    new Claim(ClaimTypes.Role,loginUser.Data.Roles),
                };

                var identity = new ClaimsIdentity (Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);
            }

            return Json(loginUser);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Home");
        }
    }
}
