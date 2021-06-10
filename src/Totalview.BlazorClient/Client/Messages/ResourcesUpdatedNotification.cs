using System.Collections.Generic;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.Messages
{
    public class ResourcesUpdatedNotification : ResourcesNotification
    {
        public ResourcesUpdatedNotification(IEnumerable<Resource> resources) : base(resources) { }
    }
}
