namespace Totalview.Communication
{
    public class TotalviewOptions
    {
        public string HostNameOrAddress { get; set; } = "localhost";

        public int Port { get; set; } = 3333;

        public int ConnectionTimeoutMilliseconds { get; set; } = 30000;

        public string UserName { get; set; } = "bac";

        public string Password { get; set; } = "1234";
    }
}
