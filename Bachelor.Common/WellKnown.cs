using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor.Common
{
    public static class WellKnown
    {
        public const string COSMOSDB_CONNECTIONSTRING_NAME = "CosmosDBConnection";
        public const string COSMOSDB_DB_NAME = "powercon";
        public const string COSMOSDB_COLLECTION_NAME_METADATA = "metadata";
        public const string COSMOSDB_COLLECTION_NAME_TELEMETRY = "telemetry";   

        public const string IOT_HUB_CONNECTION_NAME = "IoTHubConnection";
        public const string IOT_HUB_NAME = "messages/events";

        public struct EntityTypes
        {
            public static string Ops = "OpsTelemetry";

        }
    }
}
