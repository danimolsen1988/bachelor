using System;
using System.Collections.Generic;

namespace Bachelor.Common.DataModels
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string? AdOrganizationId { get; set; }
        public string? AdOrganizationName { get; set; }
        public string? FullCompanyName { get; set; }
    }
}
