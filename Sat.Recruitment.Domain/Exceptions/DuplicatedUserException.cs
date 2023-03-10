using System;

namespace Sat.Recruitment.Domain.Exceptions
{
    public class DuplicatedUserException : Exception
    {
        public DuplicatedUserException(string message) : base(message)
        {
        }
    }
}
