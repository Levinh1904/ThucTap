using Blog.ViewModels.Common;
using Blog.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.ViewModels.System.Users
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }

        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
        public List<UserViewModel> User { get; set; } = new List<UserViewModel>();
        public List<RoleViewModel> RoleViews { get; set; } = new List<RoleViewModel>();
    }
}
