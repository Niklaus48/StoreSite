using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Product.Queries.GetCategoryService
{
    public interface IGetCategoryService
    {
        ResultDto Excute();
    }

    public class CategoriesDto
    {
        public long Id { get; set; }
    }
}
