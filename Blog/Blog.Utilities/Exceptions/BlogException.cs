using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Utilities.Exceptions
{
    public class BlogException : Exception
    {
        public BlogException()
        {
        }

        public BlogException(string message)
            : base(message)
        {
        }

        public BlogException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
