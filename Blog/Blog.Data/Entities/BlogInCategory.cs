using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Entities
{
    public class BlogInCategory
    {
        public int BlogId { get; set; }

        public BLBlog Blog { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
