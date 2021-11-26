using System;
using System.Collections.Generic;

namespace SimulatorTest.DataModels
{
    public partial class Device
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? FriendlyName { get; set; }
        public string? Description { get; set; }
        public string? DeviceId { get; set; }
    }
}
