using EmployeeAppModels.BlogDummy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppServices.Defenitions.BlogDummy
{
    public interface IBlogDummyPostService
    {
        Task<BlogDummyUserPostModel> GetBlogDummyUserPost(BlogDummyUserPostRequest ObjBlogDummyUserPostRequest);
    }
}
