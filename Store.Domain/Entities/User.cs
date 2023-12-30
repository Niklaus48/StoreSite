using Store.Domain.Common;

namespace Store.Domain.Entities
{
    public class User:BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public ICollection<UserInRole> userInRoles { get; set; }
    }
}
