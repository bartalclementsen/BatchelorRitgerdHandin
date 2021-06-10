using System.Collections.Generic;

namespace Totalview.BlazorClient.Client.TotalviewCommunication
{
    public interface IDataCacheReader<T>
    {
        T? Get(int id);

        IEnumerable<T> Get();
    }
}
