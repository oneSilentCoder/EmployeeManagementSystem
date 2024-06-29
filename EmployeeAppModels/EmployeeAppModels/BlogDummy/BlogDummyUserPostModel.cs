using EmployeeAppModels.BlogDummyUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppModels.BlogDummy
{
    public class BlogDummyUserPostModel
    {
        public BlogDummyUserPostList[]? data { get; set; }
        public int total { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
    public class BlogDummyUserPostList
    {
        public string? id { get; set; }
        public string? image { get; set; }
        public int likes { get; set; }
        public string[]? tags { get; set; }
        public string? text { get; set; }
        public DateTime publishDate { get; set; }
        public BlogDummyUserList? owner { get; set; }
    }

    public class BlogDummyUserPostRequest
    {
        public string? UserId { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
