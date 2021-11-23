﻿

using Bachelor.Common;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace ManagementWebAppMVC.Services
{
    public class CosmosDBService : ICosmosDBService
    {

        public CosmosDBService(CosmosClient dbClient, string databaseName, string containerName) { 
            this._container = dbClient.GetContainer(databaseName,containerName);
        }

        private readonly Container _container;

        public async Task<T> GetItemAsync<T>(string id, string partitionKey) where T : class
        {
            try
            {
                var response = await this._container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                //consider doing this another way..
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(string queryString, string partitionKey = null) where T : class
        {
            var query = this._container.GetItemQueryIterator<T>(new QueryDefinition(queryString), 
                requestOptions: !string.IsNullOrWhiteSpace(partitionKey) ? new QueryRequestOptions { PartitionKey = new PartitionKey(partitionKey) } : null);
            var results = new List<T>();
            while (query.HasMoreResults) { 
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public static CosmosDBService InitializeCosmosClientInstance(IConfiguration configuration) {
            var databaseName = configuration["DatabaseName"];
            var containerName = configuration["ContainerName"];
            var connectionString = configuration["CosmosDBConnection"];
            var cosmosDbConnectionString = new CosmosDbConnectionString(connectionString);

            
            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(cosmosDbConnectionString.ServiceEndpoint.OriginalString, cosmosDbConnectionString.AuthKey);
            CosmosClient client = clientBuilder
                .WithConnectionModeDirect()
                .Build();
            CosmosDBService cosmosDbService = new CosmosDBService(client, databaseName, containerName);


            return cosmosDbService;
        }
    }
}