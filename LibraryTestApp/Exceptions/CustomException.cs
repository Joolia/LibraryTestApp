using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LibraryTestApp.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException()
        {
        }
        
        public CustomException(string message)
            : base(message)
        {
        }
        
        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class BookException : CustomException
    {
        public BookException()
        {
        }

        public BookException(string message)
            : base(message)
        {
        }

        public BookException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class AuthorException : CustomException
    {
        public AuthorException()
        {
        }

        public AuthorException(string message)
            : base(message)
        {
        }

        public AuthorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}