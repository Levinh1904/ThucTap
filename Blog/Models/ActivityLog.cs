using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Models
{
    public partial class ActivityLog
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}
