namespace Store.Application.Services.User.Command.LoginService
{
    public class ResultSigninDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Roles { get; set; }
    }
}
