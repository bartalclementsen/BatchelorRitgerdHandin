using System;

namespace Totalview.BlazorMvvm
{
    public class ErrorReportedEventArgs : EventArgs
    {
        public string Title { get; }

        public string Message { get; }

        public Exception? Exception { get; }

        public ErrorReportedEventArgs(string title, string message, Exception? exception = null)
        {
            Title = title;
            Message = message;
            Exception = exception;
        }
    }
}
