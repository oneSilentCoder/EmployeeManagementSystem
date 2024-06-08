using EmployeeAppModels.BlogUser;

namespace EmployeeAppServices.Defenitions.BlogUser
{
    public interface IBlogUserService
    {
        Task<BlogUserModel> GetAllBlogUserAsync();
    }
}
