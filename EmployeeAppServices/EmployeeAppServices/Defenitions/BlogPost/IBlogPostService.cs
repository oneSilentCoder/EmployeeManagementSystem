using EmployeeAppModels.BlogPost;

namespace EmployeeAppServices.Defenitions.BlogPost
{
    public interface IBlogPostService
    {
        Task<BlogPostModel> GetAllBlogPostAsync(BlogPostList ObjBlogPostList);
    }
}
