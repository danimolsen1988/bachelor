using System;
using System.Collections.Generic;

namespace Bachelor.Common.DataModels
{
    public partial class AssetDevice
    {
        public int Id { get; set; }
        public int? AssetId { get; set; }
        public int? DeviceId { get; set; }
    }
}
