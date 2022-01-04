using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Blog.Data.Entities
{
    public class Permission
    {
        [Key]
        public string FunctionId { get; set; }
        public string RoleId { get; set; }
        public string CommandId { get; set; }
    }
}
