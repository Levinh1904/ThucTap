using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Blog.Data.Entities
{
    public class CommandInFunction
    {
        [Key]
        public string CommandId { get; set; }
        public string FunctionId { get; set; }
    }
}
