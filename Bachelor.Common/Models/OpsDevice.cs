using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor.Common.Models
{
    public class OpsDevice
    {
        // <summary>
        /// The partitionKey property represents a synthetic composite partition key for the
        /// Cosmos DB container, consisting of the deviceId + current year/month/day. Using a composite
        /// key instead of simply the deviceId provides us with the following benefits:
        /// (1) Distributing the write workload at any given point in time over a high cardinality
        /// of partition keys.
        /// (2) Ensuring efficient routing on queries on a given deviceId - you can spread these across
        /// time, e.g. SELECT * FROM c WHERE c.partitionKey IN (“deviceId-2019-01-01”, “deviceId-2019-02-01”, …)
        /// (3) Scale beyond the 10GB quota for a single partition key value.
        /// </summary>
        [JsonProperty] public string partitionKey { get; set; }
        [JsonProperty] public string id { get; set; }
        [JsonProperty] public string entityType => WellKnown.EntityTypes.Ops;
        [JsonProperty] public string deviceId { get; set; }
        //All measurements are handled as a list and each sensor/subdevice has it's own id
        [JsonProperty] public List<SubDevice> measurements { get; set; }
    }
}
