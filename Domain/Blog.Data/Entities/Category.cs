using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeoAlias { get; set; }
        public string SeoDescription { get; set; }
        public int SortOrder { get; set; }
        public int? ParentId { get; set; }
        public int? NumberOfTickets { get; set; }
    }
}
