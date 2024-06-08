using EmployeeAppModels.BlogPost;
using EmployeeAppServices.Defenitions.BlogPost;
using EmployeeAppServices.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace EmployeeAPP.Controllers
{
    public class BlogPostController : Controller
    {
        #region Variable
        private readonly IBlogPostService _BlogPostService;
        #endregion

        #region Ctor
        public BlogPostController(IBlogPostService BlogPostService)
        {
            _BlogPostService = BlogPostService;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            BlogPostModel ObjBlogPostModel = await _BlogPostService.GetAllBlogPostAsync();
            return View(ObjBlogPostModel);
        }
    }
}
