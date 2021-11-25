using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor.Common.Models
{
    public class Device
    {
        public int Id { get; set; }
        public int customer_id { get; set; }
        public string friendlyName { get; set; }
        public string description { get; set; }
        public DbGeography coordinates { get; set; }
        public string device_id { get; set; }
    }
}
