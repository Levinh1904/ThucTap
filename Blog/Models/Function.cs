using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Models
{
    public partial class Function
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        public string ParentId { get; set; }
        public string Icon { get; set; }
    }
}
