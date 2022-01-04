using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Blog.Data.Entities
{

        [Table("Permissions")]
        public class Permission
        {
            public Permission(string functionId, string roleId, string commandId)
            {
                FunctionId = functionId;
                RoleId = roleId;
                CommandId = commandId;
            }

            [MaxLength(50)]
            [Key]
            public string FunctionId { get; set; }

            [MaxLength(50)]

            public string RoleId { get; set; }

            [MaxLength(50)]
            public string CommandId { get; set; }
        }
    }

