using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class SiteBudget
    {
        public int SiteId { get; set; }
        public string CostApprover { get; set; }
        public double? Amount { get; set; }

        public virtual User CostApproverNavigation { get; set; }
        public virtual Site Site { get; set; }
    }
}
