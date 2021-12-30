using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Data.Entities
{
    public class Permission
    {
        public string FunctionId { get; set; }
        public string RoleId { get; set; }
        public string CommandId { get; set; }
    }
}
