using EmployeeAppModels.BlogDummy;
using EmployeeAppServices.Defenitions.BlogDummy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EmployeeAPP.Controllers
{
    public class BlogDummyPostController : Controller
    {
        #region Variable Declaration
        private readonly IBlogDummyPostService _blogDummyPostService;
        #endregion

        #region Ctor
        public BlogDummyPostController(IBlogDummyPostService blogDummyPostService)
        {
            _blogDummyPostService = blogDummyPostService;
        }
        #endregion
        public async Task<IActionResult> Index(BlogDummyUserPostRequest ObjBlogDummyUserPostRequest)
        {
            BlogDummyUserPostModel ObjBlogDummyUserPostModel = await _blogDummyPostService.GetBlogDummyUserPost(ObjBlogDummyUserPostRequest);
            return View(ObjBlogDummyUserPostModel);
        }
    }
}
