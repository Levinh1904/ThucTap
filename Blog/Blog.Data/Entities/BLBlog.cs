using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Data.Entities
{
    public class BLBlog
    {
        public int Id { set; get; }
        public string Description { get; set; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreated { set; get; }

        public List<BlogInCategory> BlogInCategories { get; set; }


        public List<BlogTranslation> Translations { get; set; }

        public List<BlogImage> BlogImages { get; set; }

    }
}
