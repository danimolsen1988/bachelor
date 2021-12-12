using System;
using System.Collections.Generic;

namespace Bachelor.Common.DataModels
{
    public partial class Asset
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? CustomerId { get; set; }
        public int? AssetTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
