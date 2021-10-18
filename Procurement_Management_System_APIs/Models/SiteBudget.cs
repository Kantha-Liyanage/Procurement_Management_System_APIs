using System;
using System.Collections.Generic;

#nullable disable

namespace ProcurementManagmentSystemAPIs.Models
{
    public partial class SiteBudget
    {
        public SiteBudget()
        {
            Supervisors = new HashSet<Supervisor>();
        }

        public int Id { get; set; }
        public int? SiteId { get; set; }
        public int? MaterialCategoryId { get; set; }
        public double? Amount { get; set; }

        public virtual MaterialCategory MaterialCategory { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<Supervisor> Supervisors { get; set; }
    }
}
