using Microsoft.AspNetCore.Mvc;
using Store.Application.Interface.FacadePattern;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IProductFacade _productFacade;

        public CategoryController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddNewCategory(long? ParentId)
        {
            ViewBag.ParentId = ParentId;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(string Name, long? ParentId)
        {
            var newCat = _productFacade.AddNewCategoryService.Excute(ParentId, Name);
            return Json(newCat);
        }
    }
}
