using System;
using System.Collections.Generic;

namespace Bachelor.Common.DataModels
{
    public partial class Hardware
    {
        public int Id { get; set; }
        public string? HardwareId { get; set; }
        public string? Gtin { get; set; }
        public string? Mpn { get; set; }
        public string? HardwareName { get; set; }
        public string? ServiceInfo { get; set; }
    }
}
