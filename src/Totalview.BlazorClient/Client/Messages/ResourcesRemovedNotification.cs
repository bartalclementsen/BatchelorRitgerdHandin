using System.Collections.Generic;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.Messages
{
    public class ResourcesRemovedNotification : ResourcesNotification
    {
        public ResourcesRemovedNotification(IEnumerable<Resource> resources) : base(resources) { }
    }
}
