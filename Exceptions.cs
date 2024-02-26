using System;

namespace app.Matrix
{
    public class DifferentMatrixesException : Exception
    {
        public DifferentMatrixesException()
        {
        }

        public DifferentMatrixesException(string message)
            : base(message)
        {
        }

        public DifferentMatrixesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class NotASquareException : Exception
    {
        public NotASquareException()
        {
        }

        public NotASquareException(string message)
            : base(message)
        {
        }

        public NotASquareException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
