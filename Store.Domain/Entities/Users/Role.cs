using Store.Domain.Common;

namespace Store.Domain.Entities.Users
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<UserInRole> userInRoles { get; set; }
    }
}
