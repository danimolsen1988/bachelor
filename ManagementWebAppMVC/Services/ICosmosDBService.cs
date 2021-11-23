using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagementWebAppMVC.Services
{
    public interface ICosmosDBService
    {
        Task<T> GetItemAsync<T>(string id, string partitionKey) where T : class;
        Task<IEnumerable<T>> GetItemsAsync<T>(string queryString, string partitionKey = null) where T : class;
    }
}
