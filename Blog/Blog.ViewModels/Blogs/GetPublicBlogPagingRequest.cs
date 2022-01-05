using Blog.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.ViewModels.Blogs
{
    public class GetPublicBlogPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}