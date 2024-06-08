using EmployeeAppModels;
using EmployeeAppServices.Defenitions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAppApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogPostApiController : ControllerBase
    {
        private readonly IBlogPostService _BlogPostService;

        public BlogPostApiController(IBlogPostService BlogPostService)
        {
            _BlogPostService = BlogPostService;
        }

        [HttpGet("GetAllBlogPostAsync")]
        public async Task<IEnumerable<BlogPostModel>> GetAllBlogPostAsync()
        {
            return await _BlogPostService.GetAllBlogPostAsync();            
        }
    }
}
