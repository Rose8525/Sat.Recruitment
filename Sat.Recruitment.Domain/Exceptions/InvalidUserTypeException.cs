using System;

namespace Sat.Recruitment.Domain.Exceptions
{
    public class InvalidUserTypeException : Exception
    {
        public InvalidUserTypeException(string message) : base(message)
        {
        }
    }
}
