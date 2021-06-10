using System;

namespace Totalview.BlazorMvvm
{
    public interface IErrorReportingService
    {
        event EventHandler<ErrorReportedEventArgs> ErrorReported;

        void ReportError(string title, string message, Exception? exception = null);
    }

    public class ErrorReportingService : IErrorReportingService
    {
        public event EventHandler<ErrorReportedEventArgs>? ErrorReported;

        public void ReportError(string title, string message, Exception? exception = null)
        {
            ErrorReported?.Invoke(this, new ErrorReportedEventArgs(title, message, exception));
        }
    }
}
