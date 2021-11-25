using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testfunction.Models
{
    public class SubDeviceTelemetry
    {
        [JsonProperty] public string subId { get; set; }
        [JsonProperty] public double measurement { get; set; }
    }
}
