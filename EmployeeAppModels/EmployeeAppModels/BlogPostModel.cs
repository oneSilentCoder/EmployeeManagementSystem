using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppModels
{
    public class BlogPostModel
    {
        public List<BlogPostList>? BlogPostListView { get; set; }
    }

    public class BlogPostList
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string? title { get; set; }
        public string? body { get; set; }
    }
}
