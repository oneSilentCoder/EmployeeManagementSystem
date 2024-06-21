using EmployeeAppModels.BlogDummyUser;
using EmployeeAppServices.Defenitions.BlogDummyUser;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPP.Controllers
{
    public class BlogDummyUserController : Controller
    {
        #region Variable
        private readonly IBlogDummyUserService _blogDummyUserService;
        #endregion

        #region Ctor
        public BlogDummyUserController(IBlogDummyUserService blogDummyUserService)
        {
            _blogDummyUserService = blogDummyUserService;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            BlogDummyUserModel ObjBlogDummyUserModel = await _blogDummyUserService.GetAllBlogUserAsync();
            return View(ObjBlogDummyUserModel);
        }
    }
}
