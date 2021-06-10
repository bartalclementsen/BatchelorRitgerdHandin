using System.Threading.Tasks;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.TotalviewCommunication
{
    public interface ITotalviewEventHandler
    {
        Task<bool> HandleTotalviewEventAsync(TotalviewEvent totalviewEvent);
    }
}
