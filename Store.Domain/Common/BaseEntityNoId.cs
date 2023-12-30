using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Common
{
    public abstract class BaseEntityNoId
    {
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime RemoveTime { get; set; }
    }
}
