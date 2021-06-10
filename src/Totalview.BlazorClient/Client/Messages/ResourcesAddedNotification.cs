using System.Collections.Generic;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.Messages
{
    public class ResourcesAddedNotification : ResourcesNotification
    {
        public ResourcesAddedNotification(IEnumerable<Resource> resources) : base(resources) { }
    }
}
