using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Product.Command.AddNewCategoryService
{
    public interface IAddNewCategoryService
    {
        ResultDto Excute(long? ParentId, string CategoryName);
    }
}
