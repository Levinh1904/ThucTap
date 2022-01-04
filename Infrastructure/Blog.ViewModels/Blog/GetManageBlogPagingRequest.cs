using Blog.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.ViewModels.Blog
{
    public class GetManageBlogPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public int? CategoryId { get; set; }

        public string? SortOption { get; set; }
    }
}