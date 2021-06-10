using System;
using System.Collections.Generic;
using Totalview.Mediators;
using Totalview.Server;
using System.Linq;

namespace Totalview.BlazorClient.Client.Messages
{
    public abstract class StateNotification : INotification
    {
        public IDictionary<int, State> States { get; }

        public StateNotification(IEnumerable<State> resources)
        {
            States = resources?.ToDictionary(s => s.StateId, s => s) ?? throw new ArgumentNullException(nameof(resources));
        }
    }

    public class StateAddedNotification : StateNotification
    {
        public StateAddedNotification(IEnumerable<State> state) : base(state) { }
    }

    public class StateRemovedNotification : StateNotification
    {
        public StateRemovedNotification(IEnumerable<State> state) : base(state) { }
    }

    public class StateUpdatedNotification : StateNotification
    {
        public StateUpdatedNotification(IEnumerable<State> state) : base(state) { }
    }
}
