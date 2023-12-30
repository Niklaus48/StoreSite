namespace Store.Application.Services.User.Command.RegisterUser
{
    public class RequestRegisterUser
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

        public List<RolesInRegisterUserDto> Roles { get; set; }
    }
}
