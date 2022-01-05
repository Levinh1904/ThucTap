using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Entities
{
    public class Language
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public List<BlogTranslation> BlogTranslations { get; set; }

        public List<CategoryTranslation> CategoryTranslations   { get; set; }
    }
}
