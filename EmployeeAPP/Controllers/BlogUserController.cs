using EmployeeAppModels.BlogUser;
using EmployeeAppServices.Defenitions.BlogUser;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPP.Controllers
{
    public class BlogUserController : Controller
    {
        #region Variable
        private readonly IBlogUserService _BlogUserService;
        #endregion

        #region Ctor
        public BlogUserController(IBlogUserService BlogUserService)
        {
            _BlogUserService = BlogUserService;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            BlogUserModel ObjBlogUserModel = await _BlogUserService.GetAllBlogUserAsync();
            return View(ObjBlogUserModel);
        }
    }
}
