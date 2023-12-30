namespace Store.Application.Services.User.Queries.GetUser
{
    public class ResultGetUserDto
    {
        public List<GetUserDto> Users { get; set; }
        public int Rows { get; set; }
    }
}
