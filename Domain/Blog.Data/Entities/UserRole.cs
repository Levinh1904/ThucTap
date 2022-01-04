using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Blog.Data.Entities
{
    public class UserRole
    {
        [Key]
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
