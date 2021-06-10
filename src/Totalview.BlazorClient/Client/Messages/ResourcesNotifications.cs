using System;
using System.Collections.Generic;
using System.Linq;
using Totalview.Mediators;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.Messages
{
    public abstract class ResourcesNotification : INotification
    {
        public IDictionary<int, Resource> Resources { get; }

        public ResourcesNotification(IEnumerable<Resource> resources)
        {
            Resources = resources?.ToDictionary(r => r.RecId, r => r) ?? throw new ArgumentNullException(nameof(resources));
        }
    }
}
