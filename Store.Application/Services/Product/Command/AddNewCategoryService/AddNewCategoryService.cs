using Store.Application.Interface.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Product;

namespace Store.Application.Services.Product.Command.AddNewCategoryService
{
    public class AddNewCategoryService : IAddNewCategoryService
    {
        private readonly IDataBaseContext _context;

        public AddNewCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Excute(long? ParentId, string CategoryName)
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "لطفا نام را وارد کنید"
                };
            }

            var NewCategory = new Category
            {
                Name = CategoryName,
                ParentCategory = GetParent(ParentId),
            };

            _context.Categories.Add(NewCategory);
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "عملیات با موفقیت انجام شد"
            };
        }

        private Category GetParent(long? Id)
        {
            return _context.Categories.Find(Id);
        }
    }
}
