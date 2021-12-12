using System;
using System.Collections.Generic;

namespace Bachelor.Common.DataModels
{
    public partial class Device
    {
        public int Id { get; set; }
        public string? DeviceId { get; set; }
        public int? HardwareId { get; set; }
        public int? DeviceTypeId { get; set; }
        public int? UnitTypeId { get; set; }
        public int? CustomerId { get; set; }
        public string? FriendlyName { get; set; }
        public string? Description { get; set; }
    }
}
