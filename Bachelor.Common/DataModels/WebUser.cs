using System;
using System.Collections.Generic;

namespace Bachelor.Common.DataModels
{
    public partial class WebUser
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string? AdUserId { get; set; }
        public string? AdUserName { get; set; }
        public int? UserTypeId { get; set; }
    }
}
