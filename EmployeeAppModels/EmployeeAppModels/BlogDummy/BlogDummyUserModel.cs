using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppModels.BlogDummyUser
{
    public class BlogDummyUserModel
    {
        public BlogDummyUserListView[]? data { get; set; }
        public int total { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

    public class BlogDummyUserListView
    {
        public string id { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string picture { get; set; }
    }


}
