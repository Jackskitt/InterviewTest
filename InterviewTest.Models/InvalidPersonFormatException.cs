using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTest.Models
{
    public class InvalidPersonFormatException : Exception
    {
        public InvalidPersonFormatException()
        {
        }

        public InvalidPersonFormatException(string message) : base(message)
        {
        }
    }
}
