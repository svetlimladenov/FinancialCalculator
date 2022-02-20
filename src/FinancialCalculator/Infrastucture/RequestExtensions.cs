using System.Threading.Tasks;
using MassTransit;

namespace Infrastucture
{
    public static class RequestExtensions
    {
        // Based on: https://github.com/MassTransit/MassTransit/blob/develop/src/MassTransit/Clients/RequestClient.cs
        public static async Task<(Task<Response<T1>>, Task<Response<T2>>)> GetResponses<T1, T2>(this RequestHandle handle)
            where T1 : class
            where T2 : class
        {
            Task<Response<T1>> result1 = handle.GetResponse<T1>(false);
            Task<Response<T2>> result2 = handle.GetResponse<T2>();

            await Task.WhenAny(result1, result2).ConfigureAwait(false);

            return (result1, result2);
        }
    }
}
