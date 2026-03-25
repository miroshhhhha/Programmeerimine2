using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WPF
{
    public interface IApiClient
    {
        Task<List<T>> GetAsync<T>(string endpoint);
        Task PostAsync<T>(string endpoint, T data);
        Task PutAsync<T>(string endpoint, int id, T data);
        Task DeleteAsync(string endpoint, int id);
    }
}
