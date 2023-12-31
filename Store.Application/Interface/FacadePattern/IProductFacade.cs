using Store.Application.Services.Product.Command.AddNewCategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interface.FacadePattern
{
    public interface IProductFacade
    {
        IAddNewCategoryService AddNewCategoryService { get; }
    }
}
