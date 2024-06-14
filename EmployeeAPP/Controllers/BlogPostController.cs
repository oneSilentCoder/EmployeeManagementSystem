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

        //[FromQuery] is an attribute in ASP.NET Core used to indicate that a parameter should be bound from the query string of the request URL. 
        public async Task<IActionResult> Index([FromQuery] BlogPostList ObjBlogPostList)
        {           
            BlogPostModel ObjBlogPostModel = await _BlogPostService.GetAllBlogPostAsync(ObjBlogPostList);
            return View(ObjBlogPostModel);
        }
    }
}
