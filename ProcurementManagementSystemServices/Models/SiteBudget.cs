using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class SiteBudget
    {
        public int SiteId { get; set; }
        public int UserType { get; set; }
        public double? Amount { get; set; }

        public virtual Site Site { get; set; }
        public virtual UserType UserTypeNavigation { get; set; }
    }
}
