using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Models
{
    public partial class Permission
    {
        public string FunctionId { get; set; }
        public string RoleId { get; set; }
        public string CommandId { get; set; }
    }
}
