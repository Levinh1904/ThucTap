using Blog.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.ViewModels.Blogs
{
    public class GetManageBlogPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
