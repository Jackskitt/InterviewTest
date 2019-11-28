using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTest.Models
{
    public class AddressBookException : Exception
    {
        public AddressBookException()
        {
        }

        public AddressBookException(string message) : base(message)
        {
        }
    }
}
