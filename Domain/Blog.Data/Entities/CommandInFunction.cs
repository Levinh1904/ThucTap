using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Blog.Data.Entities
{
    [Table("CommandInFunctions")]
    public class CommandInFunction
    {
        [Key]
        public string CommandId { get; set; }
        public string FunctionId { get; set; }
    }
}
