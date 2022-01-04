using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Blog.Data.Entities
{
    [Table("Blogsss")]
    public class Blogss
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string SeoAlias { get; set; }
        public string Description { get; set; }
        public string Environment { get; set; }
        public string Problem { get; set; }
        public string StepToReproduce { get; set; }
        public string Workaround { get; set; }
        public string Note { get; set; }
        public string OwnerUserId { get; set; }
        public string Labels { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
