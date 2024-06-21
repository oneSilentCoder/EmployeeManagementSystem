using EmployeeAppModels.BlogDummyUser;

namespace EmployeeAppServices.Defenitions.BlogDummyUser
{
    public interface IBlogDummyUserService
    {
        Task<BlogDummyUserModel> GetAllBlogUserAsync();
    }
}
