using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagementSystemServices.Models
{
    public partial class SiteBudget
    {
        public int SiteId { get; set; }
        public int MaterialCategoryId { get; set; }
        public double? Amount { get; set; }
        public string Supervisor { get; set; }

        public virtual MaterialCategory MaterialCategory { get; set; }
        public virtual Site Site { get; set; }
        public virtual User SupervisorNavigation { get; set; }
    }
}
