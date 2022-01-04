using Blog.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.ViewModels.Blog
{
    public class BlogDetailViewModel
    {
        public CategoryViewModel Category { get; set; }

        public BlogViewModel Product { get; set; }

        //public List<ReviewViewModel> Reviews { get; set; }

        public List<ReviewViewModel> ListOfReviews { get; set; }
        public string Review { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }

        // Use to get user id when add review
        public string UserCommentId { get; set; }

        public List<BlogViewModel> RelatedProducts { get; set; }

    }
}