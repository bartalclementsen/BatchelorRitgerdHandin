using System;

namespace Totalview.Communication
{
    public class TotalviewCommunicationException : Exception
    {
        public TotalviewCommunicationException() { }

        public TotalviewCommunicationException(string? message) : base(message) { }

        public TotalviewCommunicationException(Exception? innerException) : base(null, innerException) { }

        public TotalviewCommunicationException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
