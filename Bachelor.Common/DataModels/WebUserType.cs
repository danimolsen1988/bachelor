using System;
using System.Collections.Generic;

namespace Bachelor.Common.DataModels
{
    public partial class WebUserType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? UserLevel { get; set; }
    }
}
