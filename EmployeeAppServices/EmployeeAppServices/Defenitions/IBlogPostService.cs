using EmployeeAppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppServices.Defenitions
{
    public interface IBlogPostService
    {
        Task<BlogPostModel> GetAllBlogPostAsync();
    }
}
