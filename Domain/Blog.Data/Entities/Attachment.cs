using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Data.Entities
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public int KnowledgeBaseId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
