using System;

namespace Challenger.Domain.FormulaParser.Exceptions
{
    public class WrongSyntaxException : Exception
    {
        public WrongSyntaxException()
        {

        }

        public WrongSyntaxException(string message) : base(message)
        {

        }

        public WrongSyntaxException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
