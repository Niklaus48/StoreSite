using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.User.Command.ChangeUserStatusService;
using Store.Application.Services.User.Command.DeleteUserService;
using Store.Application.Services.User.Command.EditUserService;
using Store.Application.Services.User.Command.RegisterUser;
using Store.Application.Services.User.Queries.GetRole;
using Store.Application.Services.User.Queries.GetUser;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IGetUserListService _userListService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IGetRoleService _getRoleService;
        private readonly IChangeUserStatusService _changeUserStatusService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IEditUserService _editUserService;

        public UserController(
            IGetUserListService getUserListService,
            IRegisterUserService registerUserService,
            IGetRoleService getRoleService,
            IChangeUserStatusService changeUserStatusService,
            IDeleteUserService deleteUserService,
            IEditUserService editUserService)
        {
            _userListService = getUserListService;
            _registerUserService = registerUserService;
            _getRoleService = getRoleService;
            _changeUserStatusService = changeUserStatusService;
            _deleteUserService = deleteUserService;
            _editUserService = editUserService;
        }

        public IActionResult Index(string SearchKey, int page = 1)
        {
            return View(_userListService.Excute(new RequestGetUserDto
            {
                SearchKey = SearchKey,
                Page = page
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRoleService.Excute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Email, string FullName, long RoleId, string Password, string Repassword)
        {
            var result = _registerUserService.Excute( new RequestRegisterUser
            {
                Email = Email,
                FullName = FullName,
                Roles = new List<RolesInRegisterUserDto>()
                {
                    new RolesInRegisterUserDto()
                    {
                        Id = RoleId,
                    }
                },
                Password = Password,
                RePassword = Repassword
            });
            return Json(result);
        }

        [HttpPost]
        public IActionResult ChangeStatus(long UserId)
        {
            return Json(_changeUserStatusService.Excute(UserId));
        }

        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_deleteUserService.Excute(UserId));
        }

        [HttpPost]
        public IActionResult Edit(long UserId , string FullName)
        {
            return Json(_editUserService.Excute(UserId, FullName));
        }
    }
}
