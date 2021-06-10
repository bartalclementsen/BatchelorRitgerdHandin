using System.Collections.Generic;
using System.Threading.Tasks;

namespace Totalview.BlazorClient.Client.TotalviewCommunication
{
    public interface IDataCache<T> : IDataCacheReader<T>
    {
        Task AddAsync(IEnumerable<T> resources);

        Task RemoveAsync(IEnumerable<T> resources);

        Task UpdateAsync(IEnumerable<T> resource);
    }
}
