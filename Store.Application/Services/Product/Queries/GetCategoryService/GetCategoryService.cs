using Microsoft.EntityFrameworkCore;
using Store.Application.Interface.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Product.Queries.GetCategoryService
{
    public class GetCategoryService : IGetCategoryService
    {
        private readonly IDataBaseContext _context;

        public GetCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<CategoriesDto>> Excute(long? parentId)
        {
            var categories = _context.Categories
                .Include(p => p.ParentCategory)
                .ThenInclude(p => p.SubCategories)
                .Where(p => p.ParentId == parentId)
                .ToList()
                .Select(p => new CategoriesDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Parent = p.ParentCategory != null ? new CategoryParentDto
                    {
                        Id = p.ParentCategory.Id,
                        name = p.ParentCategory.Name,
                    }
                    : null,
                    HasChild = p.SubCategories.Count > 0 ? true : false
                }).ToList();

            return new ResultDto<List<CategoriesDto>>
            {
                Data = categories,
                IsSuccess = true,
                Message = "موفق"
            };
        }
    }
}
