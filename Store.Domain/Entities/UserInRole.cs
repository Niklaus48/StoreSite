using Store.Domain.Common;

namespace Store.Domain.Entities
{
    public class UserInRole:BaseEntityNoId
    {
        public long Id { get; set; }

        public User User { get; set; }
        public long UserId { get; set; }

        public Role Role { get; set; }
        public long RoleId { get; set; }
    }
}
