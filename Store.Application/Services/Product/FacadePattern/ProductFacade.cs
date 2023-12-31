using Store.Application.Interface.Context;
using Store.Application.Interface.FacadePattern;
using Store.Application.Services.Product.Command.AddNewCategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Product.FacadePattern
{
    public class ProductFacade : IProductFacade
    {
        private readonly IDataBaseContext _context;

        public ProductFacade(IDataBaseContext context)
        {
            _context = context;
        }

        private AddNewCategoryService _AddNewCategory;
        public IAddNewCategoryService AddNewCategoryService
        {
            get
            {
                return _AddNewCategory = _AddNewCategory?? new AddNewCategoryService(_context);
            }
        }
    }
}
