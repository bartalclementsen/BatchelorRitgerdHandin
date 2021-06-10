using Totalview.Server;

namespace Totalview.BlazorClient.Client
{
    public static class StateExtensions
    {
        public static string GetHexColor(this State state)
        {
            return string.Format("#{0:x}", state.StateColor);
        }
    }
}
