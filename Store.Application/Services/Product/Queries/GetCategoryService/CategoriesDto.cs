namespace Store.Application.Services.Product.Queries.GetCategoryService
{
    public class CategoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasChild { get; set; }

        public CategoryParentDto Parent { get; set; }
    }
}
